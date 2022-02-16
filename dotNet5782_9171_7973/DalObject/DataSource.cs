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
        const int INIT_BASESTATIONS = 10;
        const int INIT_DRONES = 20;
        const int INIT_CUSTOMERS = 30;
        const int INIT_PARCELS = 40;

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
            [typeof(DroneCharge)] = DroneCharges,
            [typeof(Parcel)] = Parcels,
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
                public static readonly double Free = 0.0000001;
                public static readonly double Light = 0.0000002;
                public static readonly double Medium = 0.0000003;
                public static readonly double Heavy = 0.0000004;
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

            int chargeSlots = BaseStations.Select(s => s.ChargeSlots).Aggregate((s1, s2) => s1 + s2);
            int dronesInCharge = Math.Min(chargeSlots / 2, Drones.Count / 2);            
            
            IEnumerable<int> shuffledDrones = Drones.OrderBy(item => RandomManager.Rand.Next()).Take(dronesInCharge).Select(drone => drone.Id);
            IEnumerable<int> shuffledBaseStationsId = AvailableStationsId(dronesInCharge);
            
            // Choose some of the drones to be in charge
            DroneCharges.AddRange(
                shuffledDrones.Zip(shuffledBaseStationsId).Select(pair => new DroneCharge()
                {
                    DroneId = pair.First,
                    StationId = pair.Second,
                    StartTime = DateTime.Now,
                })
            );

            Customers.AddRange(
                Enumerable.Range(0, INIT_CUSTOMERS)
                          .Select(i => RandomManager.RandomCustomer(i))
            );

            Parcels.AddRange(
                Enumerable.Range(0, INIT_PARCELS).Select(_ => InitializeParcel()) 
            );
        }

        /// <summary>
        /// Creates an iterator which yields for each station its own id as 
        /// its charges slots number
        /// </summary>
        /// <param name="count">The number of charge slots you need</param>
        /// <returns>An <see cref="IEnumerable{int}"/></returns>
        private static IEnumerable<int> AvailableStationsId(int count)
        {
            var stationsCharges = (from station in BaseStations
                                   select Enumerable.Repeat(station.Id, station.ChargeSlots)
                                  ).SelectMany(list => list)
                                   .OrderBy(item => RandomManager.Rand.Next())
                                   .Take(count);

            foreach (var id in stationsCharges)
            {
                yield return id;
            }
        }

        /// <summary>
        /// Initialize a parcel with random values
        /// </summary>
        /// <returns>A <see cref="Parcel"/></returns>
        private static Parcel InitializeParcel()
        {
            const int ChancesOfUnAssignedParcel = 50;

            Parcel parcel = RandomManager.RandomParcel(Config.NextParcelId++, Customers);

            if (RandomManager.Rand.Next(100) < ChancesOfUnAssignedParcel)
                return parcel;

            var freeDrones = Drones.Where(drone => !Parcels.Any(parcel => parcel.DroneId == drone.Id) && !DroneCharges.Any(slot => slot.DroneId == drone.Id));

            if (!freeDrones.Any()) 
                return parcel;

            int rand = RandomManager.Rand.Next();

            return new()
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                Requested = parcel.Requested,
                DroneId = freeDrones.ElementAt(RandomManager.Rand.Next(freeDrones.Count())).Id,
                Scheduled = parcel.Requested + TimeSpan.FromHours(RandomManager.Rand.NextDouble() * 20),
                SenderId = Customers[rand % Customers.Count].Id,
                TargetId = Customers[(rand + 7) % Customers.Count].Id,
            };
        }
    }
}
