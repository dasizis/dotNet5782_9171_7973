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
        
        //number of item for arrays. (not used)
        const int DRONES = 10;
        const int BASE_STATIONS = 5;
        const int CUSTOMERS = 100;
        const int PARCELS = 1000;

        //minimum number of values to initialize 
        const int INIT_BASESTATIONS = 2;
        const int INIT_DRONES = 5;
        const int INIT_CUSTOMERS = 10;
        const int INIT_PARCELS = 10;

        //lists of information
        internal static List<Drone> drones = new List<Drone>();
        internal static List<BaseStation> baseStations = new List<BaseStation>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        internal struct Config
        {
            internal static int AvailableDrone = 1;
            internal static int AvailableStation = 1;
            internal static int AvailableCustomer = 1;
            //internal static int AvailableParcel = 1;

            internal static int ParcelId = 1;
        }

        //TODO
        /// <summary>
        /// Initialize all lists using Random func in each entity
        /// </summary>
        public static void Initialize()
        {
            for(; Config.AvailableStation < INIT_BASESTATIONS; Config.AvailableStation++)
                baseStations.Add(BaseStation.Random(Config.AvailableStation));
           
            for (; Config.AvailableDrone < INIT_DRONES; ++Config.AvailableDrone)
                drones.Add(Drone.Random(Config.AvailableDrone));
         
            for (; Config.AvailableCustomer < INIT_CUSTOMERS; ++Config.AvailableCustomer)
                customers.Add(Customer.Random(Config.AvailableCustomer));
               
            for (; Config.ParcelId < INIT_PARCELS; ++Config.ParcelId)
                parcels.Add(Parcel.Random(Config.ParcelId));
                
        }

        /// <summary>
        /// Return an item of all lists by its ID
        /// A generic function
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object GetById(Type type, int id)
        {
            switch (type.Name)
            {
                case "Drone"      : return drones.First(a => a.Id == id);
                case "BaseStation": return baseStations.First(a => a.Id == id);
                case "Customer"   : return customers.First(a => a.Id == id);
                case "Parcel"     : return parcels.First(a => a.Id == id);

                default:
                    throw new Exception("Unknown type");
            }
        }

        /// <summary>
        /// Return number of items there are in a requested list
        /// A generic function
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetNumberOfItems(Type type)
        {
            switch (type.Name)
            {
                case "Drone": return drones.Count;
                case "BaseStation": return baseStations.Count;
                case "Customer": return customers.Count;
                case "Parcel": return parcels.Count;

                default:
                    throw new Exception("Unknown type");
            }
        }

        
    }
}
