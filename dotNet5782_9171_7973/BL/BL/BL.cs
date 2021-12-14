using BO;
using Singelton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace BL
{
    public partial class BL : Singleton<BL>, BLApi.IBL
    { 
        DalApi.IDal dal { get; } = new DalApi.FactoryDL().GetDL();
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

            var dlDrones = dal.GetList<DO.Drone>().ToList();
            var parcels = dal.GetList<DO.Parcel>().ToList();
            var stationsLocations = dal.GetList<DO.BaseStation>()
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
                if (parcel.Equals(default))
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

                Location RandomSuppliedParcelLocation()
                {
                    var suppliedParcels = parcels.FindAll(p => p.Supplied.HasValue).ToList();

                    if (suppliedParcels.Count() == 0)
                    {
                        return stationsLocations[rand.Next(stationsLocations.Count())];
                    }
                    var randomParcel = suppliedParcels[rand.Next(suppliedParcels.Count())];
                    var customer = dal.GetById<DO.Customer>(randomParcel.TargetId);

                    return new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude };
                }

                // Set location
                location = state switch
                {
                    DroneState.Free => RandomSuppliedParcelLocation(),
                    DroneState.Meintenence => stationsLocations[rand.Next(stationsLocations.Count())],
                    DroneState.Deliver => parcel.Supplied != null
                                          ? targetLocation.FindClosest(stationsLocations)
                                          : senderLocation,
                };

                var availableStationsLocations = GetAvailableBaseStations()
                                                    .Select(station => GetBaseStation(station.Id).Location);

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
        //public IEnumerable<Parcel> GetNotAssignedToDroneParcels()
        //{
        //    return DataSource.Parcels.Where(parcel => parcel.DroneId == null);
        //}

        //public IEnumerable<BaseStation> GetAvailableBaseStations()
        //{
        //    return from station in DataSource.BaseStations
        //           let dronesCount = (from charge in DataSource.DroneCharges
        //                              where charge.StationId == station.Id
        //                              select charge).Count()
        //           where station.ChargeSlots > dronesCount
        //           select station.Clone();
        //}

        //public void AssignParcelToDrone(int parcelId, int droneId)
        //{
        //    Parcel parcel = GetById<Parcel>(parcelId);
        //    DataSource.Parcels.Remove(parcel);

        //    parcel.DroneId = droneId;
        //    parcel.Scheduled = DateTime.Now;
        //    DataSource.Parcels.Add(parcel);
        //}

        //public void SupplyParcel(int parcelId)
        //{
        //    Parcel parcel = GetById<Parcel>(parcelId);
        //    DataSource.Parcels.Remove(parcel);

        //    parcel.Supplied = DateTime.Now;
        //    DataSource.Parcels.Add(parcel);
        //}

        //public void ChargeDroneAtBaseStation(int droneId, int baseStationId)
        //{
        //    DataSource.DroneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = baseStationId });
        //}

        //public void FinishCharging(int droneId)
        //{
        //    var droneCharge = DataSource.DroneCharges.First(charge => charge.DroneId == droneId);
        //    DataSource.DroneCharges.Remove(droneCharge);
        //}

        //public void CollectParcel(int parcelId)
        //{
        //    Parcel parcel = GetById<Parcel>(parcelId);
        //    DataSource.Parcels.Remove(parcel);

        //    parcel.PickedUp = DateTime.Now;
        //    DataSource.Parcels.Add(parcel);
        //}
    }
}
