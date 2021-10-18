using System;




namespace DalObject
{
    public class DataSource
    {
        const int DRONES = 10;
        const int BASESTATION = 5;
        const int CUSTOMERS = 100;
        const int PARCELS = 1000;

        internal static Drone[] drones = new Drone[DRONES];
        internal static BaseStation[] baseStations = new BaseStation[BASESTATION];
        internal static Customer[] customers = new Customer[CUSTOMERS];
        internal static Parcel[] parcels = new Parcel[PARCELS];

        internal class Config
        {
            internal static int FreeDrone = 0;
            internal static int FreeStation = 0;
            //internal static int FreeCustomer = 0;
            //internal static int FreeParcel = 0;
            internal static int IdForParcel = 0;

            



        }

        //TODO
        internal static void Initialize() { }
    }

    public class DalObject
    {
        DalObject()
        {
            DataSource.Initialize();
        }

        //adding methods




    }
}
