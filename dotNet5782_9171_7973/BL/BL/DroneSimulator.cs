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

        public int Delay { get; private set; }

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
            
            if (parcel.PickedUp == null)
            {
                GoToLocation(sender.Location, bl.ElectricityConfumctiolFree);

                lock (bl)
                {
                    bl.PickUpParcel(drone.Id);
                }

                updateAction(new(Parcel: parcel.Id));
            }
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

        private void HandleMaintenanceState()
        {
            while (drone.Battery < 100)
            {
                if (shouldStop() || !SleepDelayTime(Delay)) return;

                double batteryToAdd = (double)Delay / MS_PER_SECOND * (double)bl.ChargeRate;

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

        private void HandleFreeState()
        {
            try
            {
                int parcelId = bl.AssignParcelToDrone(drone.Id);
                updateAction(new(Parcel: parcelId));
            }
            catch (InvalidActionException)
            {
                BaseStation station = drone.FindClosest(bl.GetAvailableBaseStationsId().Select(id => bl.GetBaseStation(id)));

                if (drone.Battery == 100 || station == null)
                {
                    WaitState();
                }

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

        private void GoToLocation(Location location, double electricityConfumctiol)
        {
            double distance;
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
                updateAction(new());
            }
        }

        private void WaitState()
        {
            SleepDelayTime(WAIT_TIME);
        }

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
