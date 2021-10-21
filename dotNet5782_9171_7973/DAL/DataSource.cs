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
        const int DRONES = 10;
        const int BASE_STATIONS = 5;
        const int CUSTOMERS = 100;
        const int PARCELS = 1000;

        //minimum number of values to initialize 
        const int INIT_BASESTATIONS = 2;
        const int INIT_DRONES = 5;
        const int INIT_CUSTOMERS = 10;
        const int INIT_PARCELS = 10;

        internal static List<Drone> drones = new List<Drone>();
        internal static List<BaseStation> baseStations = new List<BaseStation>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        //internal static Dictionary<string, List> data = new Dictionary<string, List<object>>()
        //{
        //    [typeof(Drone).Name]       = new List<Drone>(),
        //    [typeof(BaseStation).Name] = new List<BaseStation>(),
        //    [typeof(Customer).Name]    = new List<Customer>(),
        //    [typeof(Parcel).Name]      = new List<Parcel>(),
        //};

        internal class Config
        {
            internal static int AvailableDrone = 1;
            internal static int AvailableStation = 0;
            internal static int AvailableCustomer = 0;
            //internal static int AvailableParcel = 0;

            internal static int ParcelId = 0;
        }

        //TODO
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
