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
        const int INIT_BASESTATIONS = 2;
        const int INIT_DRONES = 5;
        const int INIT_CUSTOMERS = 10;
        const int INIT_PARCELS = 10;

        internal static List<Drone> drones = new();
        internal static List<BaseStation> baseStations = new();
        internal static List<Customer> customers = new();
        internal static List<Parcel> parcels = new();
        internal static List<DroneCharge> droneCharges = new();

        // Another way to acces the data
        internal static Dictionary<Type, IList> data = new()
        {
            [typeof(Drone)] = drones,
            [typeof(BaseStation)] = baseStations,
            [typeof(Customer)] = customers,
            [typeof(Parcel)] = parcels,
            [typeof(DroneCharge)] = droneCharges,
        };


        internal class Config
        {
            internal static int NextParcelID = 1;

            internal static class ElectricityConfumctiol
            {
                internal static int Free = 1;
                internal static int Light = 2;
                internal static int Medium = 3;
                internal static int Heavy = 4;
            }

            internal static int ChargeRate = 10;
        }

        /// <summary>
        /// initialize all the lists with random items
        /// </summary>
        public static void Initialize()
        {
            baseStations.AddRange(
                Enumerable.Range(0, INIT_BASESTATIONS)
                          .Select(i => RandomManager.RandomBaseStation(i))
            );

            drones.AddRange(
                Enumerable.Range(0, INIT_DRONES)
                          .Select(i => RandomManager.RandomDrone(i))
            );

            customers.AddRange(
                Enumerable.Range(0, INIT_CUSTOMERS)
                          .Select(i => RandomManager.RandomCustomer(i))
            );

            parcels.AddRange(
                Enumerable.Range(0, INIT_PARCELS)
                          .Select(_ => RandomManager.RandomParcel(Config.NextParcelID++))
            );

        }
    }
}
