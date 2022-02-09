﻿using BO;
using DalApi;
using System;
using System.Linq;
using System.Threading;

namespace BL
{
    class DroneSimulator
    {
        const int SECONDS_PER_HOUR = 3600;
        const int MS_PER_SECOND = 1000;
        const double KM_PER_MS = 100;


        private DroneForList drone;
        private BL bl;
        private IDal dal;

        private Action updateAction;
        private Func<bool> shouldStop;

        public int Delay { get; private set; }

        public DroneSimulator(int id, Action updateAction, Func<bool> shouldStop, int delay = 200)
        {
            bl = BL.Instance;
            dal = DalFactory.GetDal();

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
            Parcel parcel = bl.GetParcel((int)drone.DeliveredParcelId);
            Customer sender = bl.GetCustomer(parcel.Sender.Id);
            Customer target = bl.GetCustomer(parcel.Target.Id);

            if (parcel.PickedUp == null)
            {
                GoToLocation(sender.Location, bl.ElectricityConfumctiolFree);
                bl.PickUpParcel(drone.Id);
                updateAction();
            }
            else if (parcel.Supplied == null)
            {
                GoToLocation(target.Location, bl.GetElectricity(parcel.Weight));
                bl.SupplyParcel(drone.Id);
                updateAction();
            }
        }

        private void HandleMaintenanceState()
        {
            while (drone.Battery < 95)
            {
                if (shouldStop() || !SleepDelayTime()) return;

                //temp TODO
                double batteryToAdd = ((double)Delay / MS_PER_SECOND) * ((double)bl.ChargeRate / 6);
                if (drone.Battery + batteryToAdd > 100) break;

                drone.Battery += batteryToAdd;
                updateAction();
            }

            dal.DeleteWhere<DO.DroneCharge>(charge => charge.DroneId == drone.Id);
            drone.State = DroneState.Free;
        }

        private void HandleFreeState()
        {
            try
            {
                bl.AssignParcelToDrone(drone.Id);
            }
            catch (InvalidActionException)
            {
                // TODO: there is no empty base station
                BaseStation station = drone.FindClosest(bl.GetAvailableBaseStationsId().Select(id => bl.GetBaseStation(id)));
                dal.AddDroneCharge(drone.Id, station.Id);

                GoToLocation(station.Location, bl.ElectricityConfumctiolFree);
                drone.State = DroneState.Maintenance;
            }

            updateAction();
        }

        private void GoToLocation(Location location, double electricityConfumctiol)
        {
            double distance;
            while (SleepDelayTime() && (distance = Localable.Distance(drone.Location, location)) > 0)
            {
                double fraction = KM_PER_MS / distance;

                double longitudeDistance = location.Longitude - drone.Location.Longitude;
                double latitudeDistance = location.Latitude - drone.Location.Latitude;

                drone.Location = new()
                {
                    Longitude =  location.Longitude,
                    Latitude = location.Latitude,
                };
                //temp
                drone.Battery -= KM_PER_MS * electricityConfumctiol * 0.001;
                updateAction();
            }
        }

        private bool SleepDelayTime()
        {
            try
            {
                Thread.Sleep(Delay);
            }
            catch (ThreadInterruptedException)
            {
                return false; 
            }
            return true;
        }
    }
}
