using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    /// <summary>
    /// The Implementation of <see cref="BLApi.IBL"/>
    /// </summary>
    public sealed partial class BL : Singleton<BL>, BLApi.IBL
    {
        DalApi.IDal dal { get; } = DalApi.DalFactory.GetDal();

        const int MAX_CHARGE = 100;

        public Random Rand { get; } = new();

        // Electricity confumctiol properties
        public double ElectricityConfumctiolFree { get; set; }
        public double ElectricityConfumctiolLight { get; set; }
        public double ElectricityConfumctiolMedium { get; set; }
        public double ElectricityConfumctiolHeavy { get; set; }
        public double ChargeRate { get; set; }

        public List<DroneForList> drones = new();
        
        static BL() { }

        BL()
        {
            (
                ElectricityConfumctiolFree,
                ElectricityConfumctiolLight,
                ElectricityConfumctiolMedium,
                ElectricityConfumctiolHeavy,
                ChargeRate
            ) = dal.GetElectricityConfumctiol();

            var dlDrones = dal.GetList<DO.Drone>().ToList();
            var parcels = dal.GetList<DO.Parcel>().ToList();

            foreach (var dlDrone in dlDrones)
            {
                var parcel = parcels.FirstOrDefault(p => p.DroneId == dlDrone.Id);

                var availableStations = GetAvailableBaseStations();

                int rand = Rand.Next(2);

                // Does the drone have a parcel?
                if (!parcel.Equals(default(DO.Parcel)))
                {
                    drones.Add(SetDeliverDrone(dlDrone));
                }
                // Does the drone in Maintenance?
                else if (availableStations.Any() && rand == 1)
                {
                    drones.Add(SetMaintenanceDrone(dlDrone));
                }
                // Is the drone free?
                else
                {
                    drones.Add(SetFreeDrone(dlDrone));
                }
            }
        }

        private DroneForList SetDeliverDrone(DO.Drone drone)
        {
            DO.Parcel parcel = dal.GetSingle<DO.Parcel>(parcel => parcel.DroneId == drone.Id);

            var targetCustomer = dal.GetById<DO.Customer>(parcel.TargetId);
            Location targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };

            var senderCustomer = dal.GetById<DO.Customer>(parcel.SenderId);
            Location senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };

            Location location;
            double battery;

            var availableStationsLocations = from station in GetAvailableBaseStations()
                                             select GetBaseStationLocation(station.Id);

            if (availableStationsLocations.Any())
            {
                location = parcel.Supplied != null
                           ? targetLocation.FindClosest(availableStationsLocations)
                           : senderLocation;

                double neededBattery = Localable.Distance(location, senderLocation) * ElectricityConfumctiolFree +
                                           Localable.Distance(senderLocation, targetLocation) * GetElectricity((WeightCategory)parcel.Weight) +
                                           Localable.Distance(targetLocation, targetLocation.FindClosest(availableStationsLocations)) * ElectricityConfumctiolFree;

                battery = neededBattery < MAX_CHARGE ? Rand.Next((int)neededBattery, MAX_CHARGE) : MAX_CHARGE;
            }
            else
            {
                location = GetBaseStation(RandomItem(dal.GetList<DO.BaseStation>()).Id).Location;
                battery = MAX_CHARGE;
            }

            return new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (WeightCategory)drone.MaxWeight,
                Battery = battery,
                State = DroneState.Deliver,
                Location = location,
                DeliveredParcelId = parcel.Id,
            };
        }

        private DroneForList SetMaintenanceDrone(DO.Drone drone)
        {
            const double MAX_INIT_CHARGE = 20;

            var availableStations = GetAvailableBaseStations();
            var randomStation = RandomItem(availableStations);

            dal.AddDroneCharge(drone.Id, randomStation.Id);

            return new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (WeightCategory)drone.MaxWeight,
                Battery = Rand.NextDouble() * MAX_INIT_CHARGE,
                State = DroneState.Maintenance,
                Location = GetBaseStationLocation(randomStation.Id),
            };
        }

        private DroneForList SetFreeDrone(DO.Drone drone)
        {
            var suppliedParcels = dal.GetFilteredList<DO.Parcel>(p => p.Supplied != null);

            Location location;
            if (suppliedParcels.Any())
            {
                var randomParcel = RandomItem(suppliedParcels);
                var target = dal.GetById<DO.Customer>(randomParcel.TargetId);
                location = new() { Longitude = target.Longitude, Latitude = target.Latitude };
            }
            else
            {
                var randomCustomer = RandomItem(dal.GetList<DO.Customer>());
                location = new() { Longitude = randomCustomer.Longitude, Latitude = randomCustomer.Latitude };
            }

            var availableStationsLocations = from station in GetAvailableBaseStations()
                                             select GetBaseStationLocation(station.Id);

            double battery = availableStationsLocations.Count() == 0
                             ? MAX_CHARGE
                             : Rand.Next((int)(Localable.Distance(location, location.FindClosest(availableStationsLocations)) * ElectricityConfumctiolFree), MAX_CHARGE);

            return new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (WeightCategory)drone.MaxWeight,
                Battery = battery,
                State = DroneState.Free,
                Location = location,
            };
        }

        private T RandomItem<T>(IEnumerable<T> list)
        {
            return list.ElementAt(Rand.Next(list.Count()));

        }

        private Location GetBaseStationLocation(int id)
        {
            DO.BaseStation baseStation = dal.GetById<DO.BaseStation>(id);
            return new() { Longitude = baseStation.Longitude, Latitude = baseStation.Latitude };
        }


        /// <summary>
        /// Returns suitable parameter of electricity 
        /// </summary>
        /// <param name="weight">The weight category to suit to</param>
        /// <returns>The electricity confumctiol per km</returns>
        private double GetElectricity(WeightCategory weight)
        {
            return weight switch
            {
                WeightCategory.Light => ElectricityConfumctiolLight,
                WeightCategory.Medium => ElectricityConfumctiolMedium,
                WeightCategory.Heavy => ElectricityConfumctiolHeavy,
                _ => throw new ArgumentException("Invalid weight category"),
            };
        }
    }
}
