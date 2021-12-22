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

                // Set state
                if (parcel.Equals(default(DO.Parcel)))
                {
                    state = (DroneState)rand.Next(0, 2);
                }
                else
                {
                    state = DroneState.Deliver;
                    parcelInDeliverId = parcel.Id;

                    var targetCustomer = dal.GetById<DO.Customer>(parcel.TargetId);
                    targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };

                    var senderCustomer = dal.GetById<DO.Customer>(parcel.SenderId);
                    senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };

                }

                var availableStations = GetAvailableBaseStations()
                                             .Select(station => GetBaseStation(station.Id))
                                             .ToList();

                Location RandomSuppliedParcelLocation()
                {
                    var suppliedParcels = parcels.FindAll(p => p.Supplied != null).ToList();

                    if (suppliedParcels.Count == 0)
                    {
                        return availableStations[rand.Next(availableStations.Count)].Location;
                    }
                    var randomParcel = suppliedParcels[rand.Next(suppliedParcels.Count)];
                    var customer = dal.GetById<DO.Customer>(randomParcel.TargetId);

                    return new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude };
                }
                
                var randomStation = availableStations[rand.Next(availableStations.Count)];
                
                // Set location
                location = state switch
                {
                    DroneState.Free => RandomSuppliedParcelLocation(),
                    DroneState.Maintenance => randomStation.Location,
                    DroneState.Deliver => parcel.Supplied != null
                                          ? targetLocation.FindClosest(availableStations)
                                          : senderLocation,
                    _ => throw new ArgumentException("Invalid drone state"),

                };

                
                // Set battery
                battery = state switch
                {
                    DroneState.Free => rand.Next((int)((int)Location.Distance(location, location.FindClosest(availableStations)) * ElectricityConfumctiolFree), MAX_CHARGE),
                    DroneState.Maintenance => rand.NextDouble() * 20,
                    DroneState.Deliver => rand.Next(Math.Min(
                                              (int)(
                                                  Location.Distance(location, senderLocation) * ElectricityConfumctiolFree +
                                                  Location.Distance(senderLocation, targetLocation) * GetElectricity((WeightCategory)parcel.Weight) +
                                                  Location.Distance(targetLocation, targetLocation.FindClosest(availableStations)) * ElectricityConfumctiolFree
                                              ), 80)
                                             , MAX_CHARGE
                                          ),
                    _ => throw new ArgumentException("Invalid drone state"),
                };

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

                if (state == DroneState.Maintenance)
                {
                    dal.AddDroneCharge(dlDrone.Id, randomStation.Id);
                }
            }
        }

        /// <summary>
        /// returns suitable parameter of electricity 
        /// </summary>
        /// <param name="weight">weight category</param>
        /// <returns></returns>
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
