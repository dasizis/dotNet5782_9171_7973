using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace BL
{
    public partial class BL : IBL.IBL
    {
        IDAL.IDal dal { get; set; } = new DalObject.DalObject();
        const int MAX_CHARGE = 100;

        //Electricity confumctiol properties
        public double ElectricityConfumctiolFree { get; set; }
        public double ElectricityConfumctiolLight { get; set; }
        public double ElectricityConfumctiolMedium { get; set; }
        public double ElectricityConfumctiolHeavy { get; set; }
        public double ChargeRate { get; set; }

        public List<DroneForList> drones = new();
        public BL()
        {
            (
                ElectricityConfumctiolFree,
                ElectricityConfumctiolLight,
                ElectricityConfumctiolMedium,
                ElectricityConfumctiolHeavy,
                ChargeRate
            ) = dal.GetElectricityConfumctiol();

            Random rand = new();

            var dlDrones = dal.GetList<IDAL.DO.Drone>().ToList();
            var parcels = dal.GetList<IDAL.DO.Parcel>().ToList();
            var stationsLocations = dal.GetList<IDAL.DO.BaseStation>()
                                       .Select(s => new Location() { Latitude = s.Latitude, Longitude = s.Longitude })
                                       .ToList();

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
                if (parcel == null)
                {
                    state = (DroneState)rand.Next(0, 2);
                }
                else
                {
                    state = DroneState.Deliver;
                    parcelInDeliverId = parcel.Id;

                    var targetCustomer = dal.GetById<IDAL.DO.Customer>(parcel.TargetId);
                    targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };

                    var senderCustomer = dal.GetById<IDAL.DO.Customer>(parcel.SenderId);
                    senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };

                }

                Location RandomSuppliedParcelLocation()
                {
                    var suppliedParcels = parcels.FindAll(p => p.Supplied.HasValue).ToList();

                    if (suppliedParcels.Count == 0)
                    {
                        return stationsLocations[rand.Next(stationsLocations.Count)];
                    }
                    var randomParcel = suppliedParcels[rand.Next(suppliedParcels.Count)];
                    var customer = dal.GetById<IDAL.DO.Customer>(randomParcel.TargetId);

                    return new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude };
                }

                // Set location
                location = state switch
                {
                    DroneState.Free => RandomSuppliedParcelLocation(),
                    DroneState.Meintenence => stationsLocations[rand.Next(stationsLocations.Count)],
                    DroneState.Deliver => parcel.Supplied != null
                                          ? targetLocation.FindClosest(stationsLocations)
                                          : senderLocation,
                };

                var availableStationsLocations = dal.GetAvailableBaseStations()
                                                    .Select(s => new Location() { Latitude = s.Latitude, Longitude = s.Longitude })
                                                    .ToList();

                // Set battery
                battery = state switch
                {
                    DroneState.Free => rand.Next((int)((int)Location.Distance(location, location.FindClosest(availableStationsLocations)) * ElectricityConfumctiolFree), 100),
                    DroneState.Meintenence => rand.NextDouble() * 20,
                    DroneState.Deliver => rand.Next(Math.Min(
                                              (int)(
                                                  Location.Distance(location, senderLocation) * ElectricityConfumctiolFree +
                                                  Location.Distance(senderLocation, targetLocation) * GetElectricity((WeightCategory)parcel.Weight) +
                                                  Location.Distance(targetLocation, targetLocation.FindClosest(availableStationsLocations)) * ElectricityConfumctiolFree
                                              ), 80)
                                             , 100
                                          ),
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
            };
        }
    }
}
