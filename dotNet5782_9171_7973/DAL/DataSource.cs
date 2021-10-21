using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public struct DataSource
    {
        const int DRONES = 10;
        const int BASE_STATION = 5;
        const int CUSTOMERS = 100;
        const int PARCELS = 1000;

        internal static List<Drone> drones = new List<Drone>();
        internal static List<BaseStation> baseStations = new List<BaseStation>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();

        //internal static Dictionary<string, List> data = new Dictionary<string, List<object>>()
        //{
        //    [typeof(Drone).Name]       = new List<Drone>(),
        //    [typeof(BaseStation).Name] = new List<BaseStation>(),
        //    [typeof(Customer).Name]    = new List<Customer>(),
        //    [typeof(Parcel).Name]      = new List<Parcel>(),
        //};

        internal class Config
        {
            internal static int AvailableDrone = 0;
            internal static int AvailableStation = 0;
            internal static int AvailableCustomer = 0;
            internal static int AvailableParcel = 0;

            internal static int ParcelId = 0;
        }

        //TODO
        internal static void Initialize()
        {

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
    }
}
