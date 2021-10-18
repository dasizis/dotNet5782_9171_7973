using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO

namespace DalObject
{
    public struct DataSource
    {
        const int DRONES = 10;
        const int BASE_STATION = 5;
        const int CUSTOMERS = 100;
        const int PARCELS = 1000;

        internal static Drone[] drones = new Drone[DRONES];
        internal static BaseStation[] baseStations = new BaseStation[BASE_STATION];
        internal static Customer[] customers = new Customer[CUSTOMERS];
        internal static Parcel[] parcels = new Parcel[PARCELS];

        internal class Config
        {
            internal static int FreeDrone = 0;
            internal static int FreeStation = 0;
            internal static int FreeCustomer = 0;
            internal static int FreeParcel = 0;
            internal static int IdForParcel = 0;
        }

        /\tODO
        internal static void Initialize() 
        { 
        }
    }
}
