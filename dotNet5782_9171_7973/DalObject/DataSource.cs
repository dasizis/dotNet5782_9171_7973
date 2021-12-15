using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using DO;

namespace Dal
{
    public static class DataSource
    {
        const int INIT_BASESTATIONS = 2;
        const int INIT_DRONES = 20;
        const int INIT_CUSTOMERS = 10;
        const int INIT_PARCELS = 20;

        internal static List<Drone> Drones { get; } = new();
        internal static List<BaseStation> BaseStations { get; } = new();
        internal static List<Customer> Customers { get; } = new();
        internal static List<Parcel> Parcels { get; } = new();
        internal static List<DroneCharge> DroneCharges { get; } = new();

        // Another way to acces the data
        internal static Dictionary<Type, IList> Data { get; } = new()
        {
            [typeof(Drone)] = Drones,
            [typeof(BaseStation)] = BaseStations,
            [typeof(Customer)] = Customers,
            [typeof(Parcel)] = Parcels,
            [typeof(DroneCharge)] = DroneCharges,
        };

        static DataSource()
        {
            Initialize();
        }

        public class Config
        {
            public static int NextParcelID = 0;

            public static class ElectricityConfumctiol
            {
                public static double Free = 0.1;
                public static double Light = 0.2;
                public static double Medium = 0.3;
                public static double Heavy = 0.4;
            }

            public static double ChargeRate = 10;
        }

        /// <summary>
        /// initialize all the lists with random items
        /// </summary>
        public static void Initialize()
        {
            BaseStations.AddRange(
                Enumerable.Range(0, INIT_BASESTATIONS)
                          .Select(i => RandomManager.RandomBaseStation(i))
            );

            Drones.AddRange(
                Enumerable.Range(0, INIT_DRONES)
                          .Select(i => RandomManager.RandomDrone(i))
            );

            Customers.AddRange(
                Enumerable.Range(0, INIT_CUSTOMERS)
                          .Select(i => RandomManager.RandomCustomer(i))
            );

            Parcels.AddRange(
                Enumerable.Range(0, INIT_PARCELS)
                          .Select(_ => RandomManager.RandomParcel(Config.NextParcelID++, Customers))    
                          .Select(parcel => RandomManager.Rand.Next(3) == 1
                                            ? default(Parcel)
                                            : new Parcel()
                                            {
                                                Id = parcel.Id,
                                                Weight = parcel.Weight,
                                                Priority = parcel.Priority,
                                                Requested = parcel.Requested,
                                                DroneId = Drones[RandomManager.Rand.Next(Drones.Count)].Id,
                                                Scheduled = parcel.Requested + TimeSpan.FromHours(RandomManager.Rand.NextDouble() * 20),
                                                SenderId = Customers[RandomManager.Rand.Next(Customers.Count)].Id,
                                                TargetId = Customers[RandomManager.Rand.Next(Customers.Count)].Id,
                                            }

                          )

            );
        }
    }
}
