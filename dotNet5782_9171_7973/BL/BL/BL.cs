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
        public IDAL.IDal Dal { get; set; } = new DalObject.DalObject();
        public int ElectricityConfumctiolFree { get; set; }
        public int ElectricityConfumctiolLight { get; set; }
        public int ElectricityConfumctiolMedium { get; set; }
        public int ElectricityConfumctiolHeavy { get; set; }
        public int ChargeRate { get; set; }

       

        public void AddBaseStation(int id, string name, Location location, int chargeSlots)
        {
            //var station = new BaseStation()
            //{
            //    Id = id,
            //    Name = name,
            //    Location = location,
            //    EmptyChargeSlots = 0,
            //    DronesInChargeList = new(),
            //};

            Dal.Add(
                new IDAL.DO.BaseStation()
                {
                    Id = id, 
                    Name = name,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    ChargeSlots = chargeSlots,
                }
            );
        }

        public void AddCustomer(int id, string name, string phone, Location location)
        {
            Dal.Add(
                new IDAL.DO.Customer()
                {
                    Id = id,
                }
           );
        }

        public void AddDrone(int id, string model, WeightCategory maxWeight, int stationId)
        {
            throw new NotImplementedException();
        }

        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            throw new NotImplementedException();
        }

        
        public IEnumerable<BaseStation> GetAvailableBaseStations()
        {
            throw new NotImplementedException();
        }

        public object GetById(Type type, int requestedId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetList(Type type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetNotAssignedToDroneParcels()
        {
            throw new NotImplementedException();
        }

        
    }
}
