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
        /// <summary>
        /// Add a parcel to Parcels list
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            Parcel parcel = new Parcel();

            parcel.Id = DataSource.Config.ParcelId;
            parcel.SenderId = senderId;
            parcel.TargetId = targetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            parcel.Requested = DateTime.Now;
            parcel.DroneId = 0;

            DataSource.parcels.Add(parcel);
            DataSource.Config.ParcelId++;
        }

        /// <summary>
        /// Add a base station to base station list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="chargeSlots"></param>
        public void AddBaseStation(string name, double longitude, double latitude, int chargeSlots)
        {
            BaseStation baseStation = new BaseStation();

            baseStation.Id = DataSource.Config.AvailableStation;
            baseStation.Name = name;
            baseStation.Longitude = longitude;
            baseStation.Latitude = latitude;
            baseStation.ChargeSlots = chargeSlots;

            DataSource.baseStations.Add(baseStation);

            DataSource.Config.AvailableStation++;
        }

        /// <summary>
        /// add a customer to customer list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="phone"></param>
        public void AddCustomer(string name, double longitude, double latitude, string phone)
        {
            Customer customer = new Customer();

            customer.Id = DataSource.Config.AvailableCustomer;
            customer.Name = name;
            customer.Longitude = longitude;
            customer.Latitude = latitude;
            customer.Phone = phone;

            DataSource.customers.Add(customer);

            DataSource.Config.AvailableCustomer++;
        }

        /// <summary>
        /// add a drone to drone list 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="weight"></param>
        /// <param name="Status"></param>
        public void AddDrone(string model, WeightCategory weight, DroneStatus Status)
        {
            Drone drone = new Drone();

            drone.Id = DataSource.Config.AvailableDrone;
            drone.Model = model;
            drone.MaxWeight = weight;
            drone.Status = Status;
            drone.Battery = 0;

            DataSource.drones.Add(drone);

            DataSource.Config.AvailableDrone++;
        }



        //-----------Update functions---------
        /// <summary>
        /// Assign parcel to a suitable drone
        /// Update parcel and drone in their lists
        /// </summary>
        /// <param name="parcelId"></param>
        public void AssignParcelToDrone(int parcelId)
        {
            Parcel parcel = DataSource.parcels.Find(item => item.Id == parcelId);
            Drone drone =  DataSource.drones.Find(
               drone => (drone.Status == DroneStatus.Free)
               && (parcel.Weight <= drone.MaxWeight)
               );
            DataSource.parcels.Remove(parcel);
            DataSource.drones.Remove(drone);

            parcel.DroneId = drone.Id;
            if (parcel.DroneId == 0) { Console.WriteLine("drone not found"); return; }

            drone.Status = DroneStatus.Deliver;
            parcel.Scheduled = DateTime.Now;

            DataSource.parcels.Add(parcel);
            DataSource.drones.Add(drone);
        }

        /// <summary>
        /// Update parcel's pick up date
        /// </summary>
        /// <param name="parcelId"></param>
        public void CollectParcel(int parcelId)
        {
            Parcel parcel = DataSource.parcels.Find(item => item.Id == parcelId);
            if(parcel.DroneId == 0)
            {
                Console.WriteLine("Assign Parcel To Drone First");
                return;
            }

            DataSource.parcels.Remove(parcel);
            parcel.PickedUp = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// Update parcel's supply date
        /// Change drone status to FREE
        /// </summary>
        /// <param name="parcelId"></param>
        public void SupplyParcel(int parcelId)
        {
            Parcel parcel = DataSource.parcels.Find(item => item.Id == parcelId);
            if (parcel.DroneId == 0)
            {
                Console.WriteLine("Assign Parcel To Drone First, And Collect By Drone.");
                return;
            }
            Drone drone = DataSource.drones.Find(d => d.Id == parcel.DroneId);

            DataSource.drones.Remove(drone);
            DataSource.parcels.Remove(parcel);

            drone.Status = DroneStatus.Free;
            parcel.Delivered = DateTime.Now;

            DataSource.parcels.Add(parcel);
            DataSource.drones.Add(drone);
        }


        /// <summary>
        /// Put drone to charge in an available charge slot
        /// Add a charge slot to charge slot list
        /// </summary>
        /// <param name="droneId"></param>
        public void ChargeDroneAtBaseStation(int droneId)
        {
            Drone drone = DataSource.drones.Find(
               drone => (drone.Id == droneId)
               );
            //TODO: Is it an heavy action? should it be a copy of the function bellow with "FIND" only?
            //find a base station where there is an available charge slot
            BaseStation baseStation = GetStationsWithEmptySlots()[0];
            DataSource.drones.Remove(drone);

            drone.Status = DroneStatus.Meintenence;

            DroneCharge droneCharge = new DroneCharge();
            droneCharge.StationId = baseStation.Id;
            droneCharge.DroneId = drone.Id;

            DataSource.drones.Add(drone);

        }

        /// <summary>
        /// Release drone from charging at charge slot
        /// Remove charge slot from charge slot list
        /// </summary>
        /// <param name="droneId"></param>
        public void FinishCharging(int droneId)
        {
            Drone drone = DataSource.drones.Find(
               drone => (drone.Id == droneId)
               );
            DataSource.drones.Remove(drone);
            drone.Status = DroneStatus.Free;
            drone.Battery = 100;

            //remove chargeslot which charged the drone
            DataSource.droneCharges.RemoveAll(d => d.DroneId == drone.Id);
            DataSource.drones.Add(drone);
        }


        //---------return list functions---------------
        /// <summary>
        /// return base station list
        /// </summary>
        /// <returns></returns>
        public BaseStation[] GetBaseStationList()
        {
            return DataSource.baseStations.ToArray();
        }

        /// <summary>
        /// return drone list
        /// </summary>
        /// <returns></returns>
        public Drone[] GetDroneList()
        {
            return DataSource.drones.ToArray();
        }

        /// <summary>
        /// return customer list
        /// </summary>
        /// <returns></returns>
        public Customer[] GetCustomersList()
        {
            return DataSource.customers.ToArray();
        }

        /// <summary>
        /// return parcel list
        /// </summary>
        /// <returns></returns>
        public Parcel[] GetParcelList()
        {
            return DataSource.parcels.ToArray();
        }

        /// <summary>
        /// returns all parcel which were not assigned to drones yet
        /// </summary>
        /// <returns></returns>
        public Parcel[] GetParcelsNotAssignedToDrone()
        {
             return DataSource.parcels.FindAll(p => p.DroneId == 0).ToArray();
        }

        /// <summary>
        /// returns all station with empty slots for drones
        /// </summary>
        /// <returns></returns>
        public BaseStation[] GetStationsWithEmptySlots()
        {
            //find all base stations where there are available charge slots

            return DataSource.baseStations.FindAll(b => b.ChargeSlots >
            (DataSource.droneCharges.FindAll(d => d.StationId == b.Id)).Count).ToArray();
        }


    }
}
