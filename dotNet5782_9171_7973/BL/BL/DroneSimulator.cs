using BO;
using DalApi;
using System;
using System.Linq;
using System.Threading;
using static BL.BL;

namespace BL
{
    class DroneSimulator
    {
        const int SECONDS_PER_HOUR = 3600;
        const int MS_PER_SECOND = 1000;
        const double KM_PER_S = 50000;
        const int WAIT_TIME = 10_000;

        private DroneForList drone;
        private BL bl;
        private IDal dal;

        private Action<DroneSimulatorChanges> updateAction;
        private Func<bool> shouldStop;

        /// <summary>
        /// Time in miliseconds to delay between actions
        /// </summary>
        public int Delay { get; private set; }

        /// <summary>
        /// Activate drone simulator
        /// </summary>
        /// <param name="id">id of drone to activate</param>
        /// <param name="updateAction">update action to call on each change</param>
        /// <param name="shouldStop">indicates if simulator got "stop soon" order</param>
        /// <param name="delay">time in miliseconds to delay between actions</param>
        public DroneSimulator(int id, Action<DroneSimulatorChanges> updateAction, Func<bool> shouldStop, int delay = 500)
        {
            bl = BL.Instance;
            dal = bl.Dal;

            this.updateAction = updateAction;
            this.shouldStop = shouldStop;

            drone = bl.GetDroneForListRef(id);

            Delay = delay;

            while (!shouldStop())
            {
                switch (drone.State)
                {
                    case DroneState.Free:
                        HandleFreeState();
                        break;

                    case DroneState.Maintenance:
                        HandleMaintenanceState();
                        break;

                    case DroneState.Deliver:
                        HandleDeliverState();
                        break;
                }
            }
        }

        /// <summary>
        /// Handle drone with <see cref="DroneState.Deliver"/> state
        /// </summary>
        private void HandleDeliverState()
        {
            Parcel parcel;
            Customer sender;
            Customer target;
            lock (bl)
            {
                parcel = bl.GetParcel((int)drone.DeliveredParcelId);
                sender = bl.GetCustomer(parcel.Sender.Id);
                target = bl.GetCustomer(parcel.Target.Id);
            }
            
            //if drone has not pick parcel up yet
            if (parcel.PickedUp == null)
            {
                GoToLocation(sender.Location, bl.ElectricityConfumctiolFree);

                lock (bl)
                {
                    bl.PickUpParcel(drone.Id);
                }

                updateAction(new(Parcel: parcel.Id));
            }
            //if drone has not supplied parcl yet
            else if (parcel.Supplied == null)
            {
                GoToLocation(target.Location, bl.GetElectricity(parcel.Weight));

                lock (bl)
                {
                    bl.SupplyParcel(drone.Id);
                }

                updateAction(new(Parcel: parcel.Id, Customer: parcel.Target.Id));
            }
        }

        /// <summary>
        /// Handle drone with <see cref="DroneState.Maintenance"/> state
        /// </summary>
        private void HandleMaintenanceState()
        {
            //charge until battery is full
            while (drone.Battery < 100)
            {
                if (shouldStop() || !SleepDelayTime(Delay)) return;

                double batteryToAdd = (double)Delay / MS_PER_SECOND * (double)bl.ChargeRate;

                //do not charge beyond 100% 
                drone.Battery = Math.Min(drone.Battery + batteryToAdd, 100);
                updateAction(new(BaseStation: bl.GetDroneBaseStation(drone.Id)));
            }

            int stationId;
            lock (bl)
            {
                stationId = bl.FinishCharging(drone.Id);
            }
            drone.State = DroneState.Free;

            updateAction(new(BaseStation: stationId));
        }

        /// <summary>
        /// Handle drone with <see cref="DroneState.Free"/> state
        /// </summary>
        private void HandleFreeState()
        {
            try
            {
                int parcelId = bl.AssignParcelToDrone(drone.Id);
                updateAction(new(Parcel: parcelId));
            }
            //if no parcel was found to assign 
            catch (InvalidActionException)
            {
                BaseStation station = drone.FindClosest(bl.GetAvailableBaseStationsId().Select(id => bl.GetBaseStation(id)));

                //if no need to charge or no base station to charge at
                if (drone.Battery == 100 || station == null)
                {
                    WaitState();
                }
                //charge drone
                else
                {
                    lock (dal)
                    {
                        dal.AddDroneCharge(drone.Id, station.Id);
                    }

                    GoToLocation(station.Location, bl.ElectricityConfumctiolFree);
                    drone.State = DroneState.Maintenance;
                    updateAction(new(BaseStation: station.Id));
                }
            }
        }

        /// <summary>
        /// Transport drone from its location to anothe location
        /// </summary>
        /// <param name="location">location where to transport the drone to</param>
        /// <param name="electricityConfumctiol">the confumctiol the drone takes</param>
        private void GoToLocation(Location location, double electricityConfumctiol)
        {
            double distance;
            //move another part of the way if there still is way to go
            while (SleepDelayTime(Delay) && (distance = Localable.Distance(drone.Location, location)) > 0)
            {
                double fraction = Math.Min(KM_PER_S / MS_PER_SECOND, distance) / distance;

                double longitudeDistance = location.Longitude - drone.Location.Longitude;
                double latitudeDistance = location.Latitude - drone.Location.Latitude;

                drone.Location = new()
                {
                    Longitude = drone.Location.Longitude + longitudeDistance * fraction,
                    Latitude = drone.Location.Latitude + latitudeDistance * fraction,
                };
               
                drone.Battery -= Math.Min(KM_PER_S / MS_PER_SECOND, distance) * electricityConfumctiol;
                updateAction(drone.State == DroneState.Deliver ? new(Parcel: drone.DeliveredParcelId): new());
            }
        }

        /// <summary>
        /// Wait until <see cref="WAIT_TIME"/> passes
        /// </summary>
        private void WaitState()
        {
            //SleepDelayTime(WAIT_TIME);
        }

        /// <summary>
        /// Sleep simulator until delay time passes
        /// </summary>
        /// <param name="delay">time in miliseconds to sleep</param>
        /// <returns>Indicates wheather the sleep was successful</returns>
        private bool SleepDelayTime(int delay)
        {
            try
            {
                Thread.Sleep(delay);
            }
            catch (ThreadInterruptedException)
            {
                return false; 
            }
            return true;
        }
    }
}
