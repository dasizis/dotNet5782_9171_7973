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
        private DalApi.IDal dal { get; } = DalApi.DalFactory.GetDal();

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

            var dalDrones = dal.GetList<DO.Drone>();
            var parcels = dal.GetList<DO.Parcel>();
            var charges = dal.GetList<DO.DroneCharge>();

            foreach (var dalDrone in dalDrones)
            {
                var parcel = parcels.FirstOrDefault(p => p.DroneId == dalDrone.Id);

                // Does the drone have a parcel?
                if (!parcel.Equals(default(DO.Parcel)))
                {
                    drones.Add(SetDeliverDrone(dalDrone));
                }
                // Does the drone in Maintenance?
                else if (charges.Any(charge => charge.DroneId == dalDrone.Id))
                {
                    drones.Add(SetMaintenanceDrone(dalDrone));
                }
                // Is the drone free?
                else
                {
                    drones.Add(SetFreeDrone(dalDrone));
                }
            }
        }

        /// <summary>
        /// Sets all details of <see cref="DroneForList"/> which is in Delivery
        /// </summary>
        /// <param name="drone">The <see cref="DO.Drone"/></param>
        /// <returns>A <see cref="DroneForList"/></returns>
        private DroneForList SetDeliverDrone(DO.Drone drone)
        {
            DO.Parcel parcel = dal.GetSingle<DO.Parcel>(parcel => parcel.DroneId == drone.Id);

            var targetCustomer = dal.GetById<DO.Customer>(parcel.TargetId);
            Location targetLocation = new() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };

            var senderCustomer = dal.GetById<DO.Customer>(parcel.SenderId);
            Location senderLocation = GetSenderLocation(senderCustomer);

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

        private static Location GetSenderLocation(DO.Customer senderCustomer)
        {
            return new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };
        }

        /// <summary>
        /// Sets all details of <see cref="DroneForList"/> which is in maintenance
        /// </summary>
        /// <param name="drone">The <see cref="DO.Drone"/></param>
        /// <returns>A <see cref="DroneForList"/></returns>
        private DroneForList SetMaintenanceDrone(DO.Drone drone)
        {
            const double MAX_INIT_CHARGE = 20;
            var charge = dal.GetSingle<DO.DroneCharge>(c => c.DroneId == drone.Id);

            return new()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = (WeightCategory)drone.MaxWeight,
                Battery = Rand.NextDouble() * MAX_INIT_CHARGE,
                State = DroneState.Maintenance,
                Location = GetBaseStationLocation(charge.StationId),
            };
        }

        /// <summary>
        /// Sets all details of <see cref="DroneForList"/> which is free
        /// </summary>
        /// <param name="drone">The <see cref="DO.Drone"/></param>
        /// <returns>A <see cref="DroneForList"/></returns>
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

        /// <summary>
        /// Randomize an item from a list
        /// </summary>
        /// <typeparam name="T">The list generic type</typeparam>
        /// <param name="list">The list</param>
        /// <returns>A random item from the list</returns>
        private T RandomItem<T>(IEnumerable<T> list)
        {
            return list.ElementAt(Rand.Next(list.Count()));

        }

        /// <summary>
        /// Finds the base station location
        /// </summary>
        /// <param name="id">The base station id</param>
        /// <returns>The base station <see cref="Location"/></returns>
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
