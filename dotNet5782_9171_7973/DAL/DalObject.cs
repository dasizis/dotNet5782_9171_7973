using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using IDAL.DO;
using System.Linq;


namespace DalObject
{
    public struct DalObject
    {
        public static DAL.Coordinate coordinate => new DAL.Coordinate();

        //------------Adding functions----------

        /// <summary>
        /// adds a parcel to pacels list
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
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

        /// <summary>
        /// add a base station to base stations list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="chargeSlots"></param>
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

        /// <summary>
        /// adds a customer to customers list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="phone"></param>
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

        /// <summary>
        /// adds a drone to dromnes list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="weight"></param>
        /// <param name="Status"></param>
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
        /// <summary>
        /// assigns parcel to suitable drone
        /// </summary>
        /// <param name="parcelId"></param>
        public void AssignParcelToDrone(int parcelId)
        {
            Parcel parcel = DataSource.parcels.First(item => item.Id == parcelId);

            Drone drone = DataSource.drones.FirstOrDefault(
               drone => (drone.Status == DroneStatus.Free)
               && (parcel.Weight <= drone.MaxWeight)
               );
            if (drone.Equals(default))
            {
                Console.WriteLine("No Suitable Drone Found");
                return;
            }

            DataSource.parcels.Remove(parcel);
            DataSource.drones.Remove(drone);

            parcel.DroneId = drone.Id;
            drone.Status = DroneStatus.Deliver;
            parcel.Scheduled = DateTime.Now;

            DataSource.parcels.Add(parcel);
            DataSource.drones.Add(drone);

        }

        /// <summary>
        /// supplys parcel to customer
        /// </summary>
        /// <param name="parcelId"></param>
        public void SupplyParcel(int parcelId)
        {

            Parcel parcel = DataSource.parcels.First(item => item.Id == parcelId);
            if (parcel.DroneId == 0 || parcel.PickedUp == new DateTime())
                throw new Exception("Parcel Is Not In Supplying step.");

            Drone drone = DataSource.drones.Find(d => d.Id == parcel.DroneId);

            DataSource.drones.Remove(drone);
            DataSource.parcels.Remove(parcel);

            parcel.Delivered = DateTime.Now;
            drone.Status = DroneStatus.Free;

            DataSource.parcels.Add(parcel);
            DataSource.drones.Add(drone);

        }

        /// <summary>
        /// collects parcel by its drone
        /// </summary>
        /// <param name="parcelId"></param>
        public void CollectParcel(int parcelId)
        {
            Parcel parcel = DataSource.parcels.First(item => item.Id == parcelId);
            if (parcel.DroneId == 0)
                throw new Exception("Assign Parcel To Drone First");

            DataSource.parcels.Remove(parcel);
            parcel.PickedUp = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// sends drone to charge
        /// </summary>
        /// <param name="droneId"></param>
        public void ChargeDroneAtBaseStation(int droneId)
        {
            Drone drone = DataSource.drones.First(drone => drone.Id == droneId);

            drone.Status = DroneStatus.Meintenence;
            DroneCharge droneCharge = new DroneCharge();

            // find a base station where there is an available charge slot
            BaseStation baseStation = GetStationsWithEmptySlots()[0];

            droneCharge.StationId = baseStation.Id;
            droneCharge.DroneId = drone.Id;

            DataSource.droneCharges.Add(droneCharge);
        }

        /// <summary>
        /// releases drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        public void FinishCharging(int droneId)
        {
            Drone drone = DataSource.drones.First(drone => drone.Id == droneId);
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

        /// <summary>
        /// finds all parcels not assigned to drones yet
        /// </summary>
        /// <returns>array of found parcels</returns>
        public Parcel[] GetParcelsNotAssignedToDrone()
        {
             return DataSource.parcels.FindAll(p => p.DroneId == 0).ToArray();
        }

        /// <summary>
        /// finds all station with available charge slots
        /// </summary>
        /// <returns>the array of stations found</returns>
        public BaseStation[] GetStationsWithEmptySlots()
        {
            //find all base stations where there are available charge slots

            return DataSource.baseStations.FindAll(b => b.ChargeSlots >
            DataSource.droneCharges.FindAll(d => d.StationId == b.Id).Count).ToArray();
        }


    }
}
