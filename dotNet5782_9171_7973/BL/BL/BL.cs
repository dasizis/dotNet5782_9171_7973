using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace BL
{
    public partial class BL : IBL.IBL
    {
        IDAL.IDal Dal { get; set; } = new DalObject.DalObject();
        const int MAX_CHARGE = 100;

        //Electricity confumctiol properties
        public double ElectricityConfumctiolFree { get; set; }
        public double ElectricityConfumctiolLight { get; set; }
        public double ElectricityConfumctiolMedium { get; set; }
        public double ElectricityConfumctiolHeavy { get; set; }
        public double ChargeRate { get; set; }

        /// <summary>
        /// add a base station 
        /// </summary>
        /// <param name="id">the base station id</param>
        /// <param name="name">the base station name</param>
        /// <param name="location">the base station location</param>
        /// <param name="chargeSlots">the base station number of charge slots</param>
        public void AddBaseStation(int id, string name, Location location, int chargeSlots)
        {
            var station = new BaseStation()
            {
                Id = id,
                Name = name,
                Location = location,
                EmptyChargeSlots = chargeSlots,
                DronesInChargeList = new(),
            };

            Dal.Add(
                new IDAL.DO.BaseStation()
                {
                    Id = station.Id,
                    Name = station.Name,
                    Latitude = station.Location.Latitude,
                    Longitude = station.Location.Longitude,
                    ChargeSlots = station.EmptyChargeSlots + station.DronesInChargeList.Count,
                }
            );
        }
        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="id">the customer id</param>
        /// <param name="name">the customer name</param>
        /// <param name="phone">the customer phone number</param>
        /// <param name="location">the customer location</param>
        public void AddCustomer(int id, string name, string phone, Location location)
        {
            var customer = new Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Location = location,
                Send = new(),
                Recieve = new(),
            };

            Dal.Add(
                new IDAL.DO.Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Longitude = customer.Location.Longitude,
                    Latitude = customer.Location.Latitude,
                }
           );
        }
        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="id">the drone id</param>
        /// <param name="model">the drone model </param>
        /// <param name="maxWeight">the drone max weight to carry</param>
        /// <param name="stationId">first station for drone first charge</param>
        public void AddDrone(int id, string model, WeightCategory maxWeight, int stationId)
        {
            var station = GetBaseStation(stationId);

            var drone = new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                Battery = 0,
                Location = station.Location,
                ParcelInDeliver = null,
                State = DroneState.MEINTENENCE,
            };

            drones.Add(new DroneForList()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = drone.MaxWeight,
                Battery = drone.Battery,
                Location = new Location() { Latitude = drone.Location.Latitude, Longitude = drone.Location.Longitude },
                DeliveredParcelId = null,
                State = drone.State,
            });

            Dal.Add(new IDAL.DO.Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (IDAL.DO.WeightCategory)drone.MaxWeight,
            });
        }
        /// <summary>
        /// add a parcel
        /// </summary>
        /// <param name="senderId">the parcel sender customer id</param>
        /// <param name="targetId">the parcel target customer id</param>
        /// <param name="weight">the parcel weight</param>
        /// <param name="priority">the parcel priority</param>
        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            var parcel = new Parcel()
            {
                Id = Dal.GetParcelContNumber(),
                Priority = priority,
                Weight = weight,
                Sender = GetCustomerInDelivery(senderId),
                Target = GetCustomerInDelivery(targetId),
                Requested = DateTime.Now,
            };

            Dal.Add(new IDAL.DO.Parcel()
            {
                Id = parcel.Id,
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Target.Id,
                Priority = (IDAL.DO.Priority)parcel.Priority,
                Weight = (IDAL.DO.WeightCategory)parcel.Weight,
                DroneId = null,
                Requested = parcel.Requested,
            });
        }
        /// <summary>
        /// return list of base stations with empty charge slots
        /// </summary>
        /// <returns>list of base stations with empty charge slots</returns>
        public IEnumerable<BaseStationForList> GetAvailableBaseStations()
        {
            return Dal.GetAvailableBaseStations().Select(station => GetBaseStationForList(station.Id));
        }

        //public T GetById<T>(int id) where T :IDAL.DO.IIdentifiable
        //{
        //    Type blType = typeof(BL);
        //    MethodInfo getMethod = blType.GetMethod(name: $"Get{typeof(T).Name}", types: new Type[] { typeof(int) });

        //    return (T)getMethod.Invoke(this, new object[] { id });
        //}
        //public IEnumerable<T> GetList<T>() where T : IDAL.DO.IIdentifiable
        //{
        //    Type blType = typeof(BL);
        //    MethodInfo getMethod = blType.GetMethod(name: $"Get{typeof(T).Name}", types: new Type[] { typeof(int) });

        //    return Dal.GetList<T>().Select(item => (T)getMethod.Invoke(this, new object[] { item.Id } ));
        //}

        /// <summary>
        /// return list of parcels which weren't assigned to drone yet
        /// </summary>
        /// <returns>list of parcels which weren't assigned to drone yet</returns>
        public IEnumerable<ParcelForList> GetNotAssignedToDroneParcels()
        {
            return Dal.GetNotAssignedToDroneParcels().Select(parcel => GetParcelForList(parcel.Id));
        }
        /// <summary>
        /// return customers list
        /// </summary>
        /// <returns>customers list</returns>
        public IEnumerable<CustomerForList> GetCustomersList()
        {
            return Dal.GetList<IDAL.DO.Customer>().Select(customer => GetCustomerForList(customer.Id));
        }
        /// <summary>
        /// return base stations list
        /// </summary>
        /// <returns>base stations list</returns>
        public IEnumerable<BaseStationForList> GetBaseStationsList()
        {
            return Dal.GetList<IDAL.DO.BaseStation>().Select(baseStation => GetBaseStationForList(baseStation.Id));
        }
        /// <summary>
        /// return drone list
        /// </summary>
        /// <returns>drone list</returns>
        public IEnumerable<DroneForList> GetDronesList()
        {
            return Dal.GetList<IDAL.DO.Drone>().Select(drone => GetDroneForList(drone.Id));
        }
        /// <summary>
        /// return parcels list
        /// </summary>
        /// <returns>parcels list</returns>
        public IEnumerable<ParcelForList> GetParcelsList()
        {
            return Dal.GetList<IDAL.DO.Parcel>().Select(parcel => GetParcelForList(parcel.Id));
        }
    }
}
