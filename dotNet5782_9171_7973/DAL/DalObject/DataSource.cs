using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public static class DataSource
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

        static DataSource()
        {
            Initialize();
        }

        internal class Config
        {
            internal static int NextParcelID = 1;

            internal static class ElectricityConfumctiol
            {
                internal static double Free = 0.1;
                internal static double Light = 0.2;
                internal static double Medium = 0.3;
                internal static double Heavy = 0.4;
            }

            internal static double ChargeRate = 10;
        }

        /// <summary>
        /// initialize all the lists with random items
        /// </summary>
        public static void Initialize()
        {
            baseStations.AddRange(
                Enumerable.Range(1, INIT_BASESTATIONS)
                          .Select(i => RandomManager.RandomBaseStation(i))
            );

            drones.AddRange(
                Enumerable.Range(1, INIT_DRONES)
                          .Select(i => RandomManager.RandomDrone(i))
            );

            customers.AddRange(
                Enumerable.Range(1, INIT_CUSTOMERS)
                          .Select(i => RandomManager.RandomCustomer(i))
            );

            parcels.AddRange(
                Enumerable.Range(1, INIT_PARCELS)
                          .Select(_ => RandomManager.RandomParcel(Config.NextParcelID++, customers))    
                          .Select(parcel => RandomManager.Rand.Next(2) == 1
                                            ? parcel
                                            : new Parcel()
                                            {
                                                Id = parcel.Id,
                                                Weight = parcel.Weight,
                                                Priority = parcel.Priority,
                                                Requested = parcel.Requested,
                                                DroneId = drones[RandomManager.Rand.Next(drones.Count)].Id,
                                                Scheduled = parcel.Requested + TimeSpan.FromHours(RandomManager.Rand.NextDouble() * 20),
                                                SenderId = customers[RandomManager.Rand.Next(customers.Count)].Id,
                                                TargetId = customers[RandomManager.Rand.Next(customers.Count)].Id,
                                            }

                          )

            );
        }
    }
}
