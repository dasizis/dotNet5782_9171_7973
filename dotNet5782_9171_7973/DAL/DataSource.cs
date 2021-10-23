using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public struct DataSource
    {
        // Minimum number of values to initialize 
        const int INIT_BASESTATIONS = 2;
        const int INIT_DRONES = 5;
        const int INIT_CUSTOMERS = 10;
        const int INIT_PARCELS = 10;

        internal static List<Drone> drones = new List<Drone>();
        internal static List<BaseStation> baseStations = new List<BaseStation>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        internal class Config
        {
            internal static int NextDroneID = 0;
            internal static int NextBaseStationID = 0;
            internal static int NextCustomerID = 0;
            internal static int NextParcelID = 0;
        }

        /// <summary>
        /// initialize all the lists with random items
        /// </summary>
        public static void Initialize()
        {
            for(; baseStations.Count < INIT_BASESTATIONS; ++Config.NextBaseStationID)
            {
                baseStations.Add(BaseStation.Random(Config.NextBaseStationID));
            }
           
            for (; drones.Count < INIT_DRONES; ++Config.NextDroneID)
            {
                drones.Add(Drone.Random(Config.NextDroneID));
            }

            for (; customers.Count < INIT_CUSTOMERS; ++Config.NextCustomerID)
            {
                customers.Add(Customer.Random(Config.NextCustomerID));
            }

            for (; parcels.Count < INIT_PARCELS; ++Config.NextParcelID)
            {
                parcels.Add(Parcel.Random(Config.NextParcelID));
            }
        }

        /// <summary>
        /// finds the element of a given type with the wanted id otherwise finds the 
        /// first item from this type (mistake, should throw an exeption)
        /// </summary>
        /// <param name="type">the item type</param>
        /// <param name="id">the item id</param>
        /// <returns></returns>
        public static object GetById(Type type, int id)
        {
            switch (type.Name)
            {
                case "Drone"      : return drones.Find(a => a.Id == id);
                case "BaseStation": return baseStations.Find(a => a.Id == id);
                case "Customer"   : return customers.Find(a => a.Id == id);
                case "Parcel"     : return parcels.Find(a => a.Id == id);

                default:
                    throw new Exception("Unknown type");
            }
        }

        //public static object GetNumberOfItems(Type type)
        //{
        //    switch (type.Name)
        //    {
        //        case "Drone": return drones.Count;
        //        case "BaseStation": return baseStations.Count;
        //        case "Customer": return customers.Count;
        //        case "Parcel": return parcels.Count;

        //        default:
        //            throw new Exception("Unknown type");
        //    }
        //}
    }
}
