using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DO;

namespace Dal
{
    /// <summary>
    /// Resotres the data for the <see cref="DalObject"/>
    /// implementaion of <see cref="DalApi.IDal"/>
    /// </summary>
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

        // Another way to access the data
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

        public static class Config
        {
            public static int NextParcelId = 0;

            public static class ElectricityConfumctiol
            {
                public static readonly double Free = 0.01;
                public static readonly double Light = 0.02;
                public static readonly double Medium = 0.03;
                public static readonly double Heavy = 0.04;
            }

            public static readonly double ChargeRate = 40;
        }

        /// <summary>
        /// Initialize all the lists with random items
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
                Enumerable.Range(0, INIT_PARCELS).Select(_ => InitilzeParcel()) 
            );
        }

        private static Parcel InitilzeParcel()
        {
            const int ChancesOfUnAssignedParcel = 50;

            Parcel parcel = RandomManager.RandomParcel(Config.NextParcelId++, Customers);

            if (RandomManager.Rand.Next(100) < ChancesOfUnAssignedParcel)
                return parcel;

            var notDeliveredDrones = Drones.Where(drone => !Parcels.Any(parcel => parcel.DroneId == drone.Id));

            return new()
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                Requested = parcel.Requested,
                DroneId = notDeliveredDrones.ElementAt(RandomManager.Rand.Next(notDeliveredDrones.Count())).Id,
                Scheduled = parcel.Requested + TimeSpan.FromHours(RandomManager.Rand.NextDouble() * 20),
            };
        }
    }
}
