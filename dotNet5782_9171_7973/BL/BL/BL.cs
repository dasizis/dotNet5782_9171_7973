using IBL.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
     public partial class BL: IBL.IBL
     {
        IDAL.IDal Dal { get; set; } = new DalObject.DalObject();
        const int MAX_CHARGE = 100;
        public int ElectricityConfumctiolFree { get; set; }
        public int ElectricityConfumctiolLight { get; set; }
        public int ElectricityConfumctiolMedium { get; set; }
        public int ElectricityConfumctiolHeavy { get; set; }
        public int ChargeRate { get; set; }

       

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

        public void AddDrone(int id, string model, WeightCategory maxWeight, int stationId)
        {
            var station = GetById<BaseStation>(stationId);

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
                Location = new Location() { Latitude = drone.Location.Latitude, Longitude = drone.Location.Longitude},
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

        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            var parcel = new Parcel()
            {
                Id = Dal.GetParcelContNumber(),
                Priority = priority,
                Weight = weight,
                Sender = GetById<CustomerInDelivery>(senderId),
                Target = GetById<CustomerInDelivery>(targetId),
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

        
        public IEnumerable<BaseStation> GetAvailableBaseStations()
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : IDAL.DO.IIdentifiable
        {
            return typeof(T) switch
            {
                var t when t == typeof(Drone) => GetDrone(id),
                var t when t == typeof(DroneForList) => GetDrone(id),
                var t when t == typeof(DroneInCharge) => GetDrone(id),
                var t when t == typeof(DroneInDeliver) => GetDrone(id),
                var t when t == typeof() => GetDrone(id),
                var t when t == typeof(Drone) => GetDrone(id),
                var t when t == typeof(Drone) => GetDrone(id),
                var t when t == typeof(Drone) => GetDrone(id),
                var t when t == typeof(Drone) => GetDrone(id),
            };
        }

        public IEnumerable<T> GetList<T>() where T : IDAL.DO.IIdentifiable
        {
            return Dal.GetList<T>().Select(item => GetById<T>(item.Id));
        }


        public IEnumerable<Parcel> GetNotAssignedToDroneParcels()
        {
            throw new NotImplementedException();
        }

        
    }
}
