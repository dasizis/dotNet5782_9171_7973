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

        // Electricity confumctiol properties
        public double ElectricityConfumctiolFree { get; set; }
        public double ElectricityConfumctiolLight { get; set; }
        public double ElectricityConfumctiolMedium { get; set; }
        public double ElectricityConfumctiolHeavy { get; set; }
        public double ChargeRate { get; set; }

        public List<DroneForList> drones = new();
        
        BL()
        {
            (
                ElectricityConfumctiolFree,
                ElectricityConfumctiolLight,
                ElectricityConfumctiolMedium,
                ElectricityConfumctiolHeavy,
                ChargeRate
            ) = dal.GetElectricityConfumctiol();

            Random rand = new();

            var dlDrones = dal.GetList<DO.Drone>().ToList();
            var parcels = dal.GetList<DO.Parcel>().ToList();

            foreach (var dlDrone in dlDrones)
            {

                var parcel = parcels.FirstOrDefault(p => p.DroneId == dlDrone.Id);
                double battery;
                int? parcelInDeliverId = null;
                DroneState state;
                Location location;
                Location targetLocation = null;
                Location senderLocation = null;

                var availableStations = GetAvailableBaseStations()
                                             .Select(station => GetBaseStation(station.Id))
                                             .ToList();

                // Set state
                if (parcel.Equals(default(DO.Parcel)))
                {
                    if (availableStations.Count == 0) state = DroneState.Free;
                    else state = (DroneState)rand.Next(0, 2);
                }
                else
                {
                    state = DroneState.Deliver;
                }

                // Set Battery & Location
                if (state == DroneState.Free)
                {
                    var suppliedParcels = parcels.FindAll(p => p.Supplied != null).ToList();

                    if (suppliedParcels.Count == 0)
                    {
                        var stations = dal.GetList<DO.BaseStation>().ToList();
                        var randomStation = stations[rand.Next(stations.Count)];
                        location = GetBaseStation(randomStation.Id).Location;
                    }
                    else
                    {
                        var randomParcel = suppliedParcels[rand.Next(suppliedParcels.Count)];
                        var customer = dal.GetById<DO.Customer>(randomParcel.TargetId);
                        location = new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude };
                    }

                    battery = rand.Next((int)(Localable.Distance(location, location.FindClosest(availableStations)) * ElectricityConfumctiolFree), MAX_CHARGE);
                }
                else if (state == DroneState.Maintenance)
                {
                    var selectedStation = availableStations[rand.Next(availableStations.Count)];
                    location = selectedStation.Location;
                    dal.AddDroneCharge(dlDrone.Id, selectedStation.Id);
                    battery = rand.NextDouble() * 20;
                }
                else if (state == DroneState.Deliver)
                {
                    parcelInDeliverId = parcel.Id;

                    var targetCustomer = dal.GetById<DO.Customer>(parcel.TargetId);
                    targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };

                    var senderCustomer = dal.GetById<DO.Customer>(parcel.SenderId);
                    senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };

                    location = parcel.Supplied != null
                               ? targetLocation.FindClosest(availableStations)
                               : senderLocation;

                    battery = availableStations.Count == 0
                              ? MAX_CHARGE
                              : rand.Next((int)(Localable.Distance(location, senderLocation) * ElectricityConfumctiolFree +
                                         Localable.Distance(senderLocation, targetLocation) * GetElectricity((WeightCategory)parcel.Weight) +
                                         Localable.Distance(targetLocation, targetLocation.FindClosest(availableStations)) * ElectricityConfumctiolFree)
                                         , MAX_CHARGE);
                }
                else
                {
                    throw new ArgumentException("Invalid drone state");
                }

                drones.Add(
                        new DroneForList()
                        {
                            Id = dlDrone.Id,
                            Model = dlDrone.Model,
                            MaxWeight = (WeightCategory)dlDrone.MaxWeight,
                            Battery = battery,
                            State = state,
                            Location = location,
                            DeliveredParcelId = parcelInDeliverId
                        }
                );
            }
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
