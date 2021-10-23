using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using IDAL.DO;


namespace DalObject
{
    public struct DalObject
    {

        //------------Adding functions----------

        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            Parcel parcel = new Parcel
            {
                Id = DataSource.Config.NextParcelID++,
                SenderId = senderId,
                TargetId = targetId,
                Weight = weight,
                Priority = priority,
                Requested = DateTime.Now,
                DroneId = 0
            };

            DataSource.parcels.Add(parcel);
        }

        public void AddBaseStation(string name, double longitude, double latitude, int chargeSlots)
        {
            BaseStation baseStation = new BaseStation
            {
                Id = DataSource.Config.NextBaseStationID++,
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = chargeSlots
            };

            DataSource.baseStations.Add(baseStation);
        }

        public void AddCustomer(string name, double longitude, double latitude, string phone)
        {
            Customer customer = new Customer();

            customer.Id = DataSource.Config.NextCustomerID++;
            customer.Name = name;
            customer.Longitude = longitude;
            customer.Latitude = latitude;
            customer.Phone = phone;

            DataSource.customers.Add(customer);
        }

        public void AddDrone(string model, WeightCategory weight, DroneStatus Status)
        {
            Drone drone = new Drone();

            drone.Id = DataSource.Config.NextDroneID++;
            drone.Model = model;
            drone.MaxWeight = weight;
            drone.Status = Status;
            drone.Battery = 0;

            DataSource.drones.Add(drone);
        }

        // ----------- Update functions ---------
        public void AssignParcelToDrone(int parcelId)
        {
            Parcel parcel = DataSource.parcels.Find(item => item.Id == parcelId);
            DataSource.parcels.Remove(parcel);
            Drone drone =  DataSource.drones.Find(
               drone => (drone.Status == DroneStatus.Free) && (parcel.Weight <= drone.MaxWeight)
            );

            drone.Status = DroneStatus.Deliver;
            parcel.DroneId = drone.Id;
            parcel.Scheduled = DateTime.Now;
            DataSource.parcels.Add(parcel);
            DataSource.drones.Add(drone);
        }

        public void CollectParcel(int parcelId)
        {
            Parcel parcel = DataSource.parcels.Find(item => item.Id == parcelId);
            parcel.PickedUp = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        public void ChargeDroneAtBaseStation(int droneId)
        {
            Drone drone = DataSource.drones.Find(drone => drone.Id == droneId);

            drone.Status = DroneStatus.Meintenence;
            DroneCharge droneCharge = new DroneCharge();

            // find a base station where there is an available charge slot
            BaseStation baseStation = GetStationsWithEmptySlots()[0];

            //TODO: Is it an heavy action? should it be a copy of the function bellow with "FIND" only?
            droneCharge.StationId = baseStation.Id;
            droneCharge.DroneId = drone.Id;

            DataSource.drones.Add(drone);

        }

        
        public void FinishCharging(int droneId)
        {
            Drone drone = DataSource.drones.Find(drone => drone.Id == droneId);
            DataSource.drones.Remove(drone);
            drone.Status = DroneStatus.Free;
            drone.Battery = 100;

            //remove chargeslot which charged the drone
            DataSource.droneCharges.RemoveAll(d => d.DroneId == drone.Id);
            DataSource.drones.Add(drone);
        }


        //---------return list functions---------------

        /// <summary>
        /// returns the base stations list as array
        /// </summary>
        /// <returns>an array of all the exist base stations</returns>
        public BaseStation[] GetBaseStationList()
        {
            return DataSource.baseStations.ToArray();
        }

        /// <summary>
        /// returns the drones list as array
        /// </summary>
        /// <returns>an array of all the exist drones</returns>
        public Drone[] GetDroneList()
        {
            return DataSource.drones.ToArray();
        }

        /// <summary>
        /// returns the customers list as array
        /// </summary>
        /// <returns>an array of all the exist customers</returns>
        public Customer[] GetCustomersList()
        {
            return DataSource.customers.ToArray();
        }

        /// <summary>
        /// returns the parcels list as array
        /// </summary>
        /// <returns>an array of all the exist parcels</returns>
        public Parcel[] GetParcelList()
        {
            return DataSource.parcels.ToArray();
        }

        public Parcel[] GetParcelsNotAssignedToDrone()
        {
             return DataSource.parcels.FindAll(p => p.DroneId == 0).ToArray();
        }

        public BaseStation[] GetStationsWithEmptySlots()
        {
            //find all base stations where there are available charge slots

            return DataSource.baseStations.FindAll(b => b.ChargeSlots >
            DataSource.droneCharges.FindAll(d => d.StationId == b.Id).Count).ToArray();
        }


    }
}
