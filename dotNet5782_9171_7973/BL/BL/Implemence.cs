using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    partial class BL
    {
        List<DroneForList> drones = new();
        
        public BL()
        {
            (
                ElectricityConfumctiolFree,
                ElectricityConfumctiolLight,
                ElectricityConfumctiolMedium,
                ElectricityConfumctiolHeavy,
                ChargeRate
            ) = Dal.GetElectricityConfumctiol();

            Random rand = new();

            var dlDrones = Dal.GetList<IDAL.DO.Drone>().ToList();
            var parcels = Dal.GetList<IDAL.DO.Parcel>().ToList();
            var stationsLocations = Dal.GetList<IDAL.DO.BaseStation>()
                                       .Select(s => new Location() { Latitude = s.Latitude, Longitude = s.Longitude })
                                       .ToList();

            foreach (var dlDrone in dlDrones)
            {

                var parcel = parcels.FirstOrDefault(parcel => parcel.DroneId == dlDrone.Id);
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
                    state = DroneState.DELIVER;
                    parcelInDeliverId = parcel.Id;

                    var targetCustomer = Dal.GetById<IDAL.DO.Customer>(parcel.TargetId);
                    targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };

                    var senderCustomer = Dal.GetById<IDAL.DO.Customer>(parcel.SenderId);
                    senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };
                    
                }

                Location RandomSuppliedParcelLocation()
                {
                    var suppliedParcels = parcels.FindAll(p => p.Supplied != null).ToList();
                    var randomParcel = suppliedParcels[rand.Next(suppliedParcels.Count)];

                    var customer = Dal.GetById<IDAL.DO.Customer>(randomParcel.TargetId);

                    return new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude };
                }

                // Set location
                location = state switch
                {
                    DroneState.Free => RandomSuppliedParcelLocation(),
                    DroneState.MEINTENENCE => stationsLocations[rand.Next(stationsLocations.Count)],
                    DroneState.DELIVER => parcel.Supplied != null
                                          ? targetLocation.FindClosest(stationsLocations)
                                          : senderLocation,
                };

                var availableStationsLocations = Dal.GetAvailableBaseStations()
                                                    .Select(s => new Location() { Latitude = s.Latitude, Longitude = s.Longitude })
                                                    .ToList();

                // Set battery
                battery = state switch
                {
                    DroneState.Free => rand.Next((int)((int)Location.Distance(location, location.FindClosest(availableStationsLocations)) * ElectricityConfumctiolFree), 100),
                    DroneState.MEINTENENCE => rand.NextDouble() * 20,
                    DroneState.DELIVER => rand.Next(Math.Min(
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
        /// find a suitable parcel and assigns it to the drone
        /// </summary>
        /// <param name="droneId">drone id to assign a parcel to</param>
        public void AssignParcelToDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);

            if (drone.State == DroneState.DELIVER)
            {
                throw new InValidActionException();
            }

           var parcels = (Dal.GetNotAssignedToDroneParcels() as List<IDAL.DO.Parcel>)
                         .FindAll(parcel => 
                              (int)parcel.Weight < (int)drone.MaxWeight && 
                              IsAbleToPassParcel(drone, GetParcelInDeliver(parcel.Id)))
                         .OrderBy(p => p.Priority)
                         .ThenBy(p => p.Weight)
                         .ThenBy(p => Location.Distance(GetCustomer(p.SenderId).Location, drone.Location))
                         .ToList();

            if (parcels.Count == 0)
            {
                throw new InValidActionException();
            }

            Dal.AssignParcelToDrone(parcels.First().Id, droneId);

            drone.State = DroneState.DELIVER;
        }
        /// <summary>
        /// release drone from charging
        /// </summary>
        /// <param name="droneId">drone to release</param>
        /// <param name="timeInCharge">time drone was in charge</param>
        public void FinishCharging(int droneId, double timeInCharge)
        {
            DroneForList drone = GetDroneForList(droneId);

            if (drone.State != DroneState.MEINTENENCE)
            {
                throw new InValidActionException();
            }

            drone.Battery += ChargeRate * timeInCharge;
            drone.State = DroneState.Free;

            Dal.FinishCharging(drone.Id);
        }
        /// <summary>
        /// drone picks up his assigned parcel
        /// </summary>
        /// <param name="droneId">drone picks</param>
        public void PickUpParcel(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);

            // Does the drone have a parcel?
            if (!drone.DeliveredParcelId.HasValue)
            {                
                throw new InValidActionException();
            }

            var parcel = Dal.GetById<IDAL.DO.Parcel>(drone.DeliveredParcelId.Value);

            // Was the parcel collected?
            if(parcel.PickedUp != null)
            {
                throw new InValidActionException();
            }

            ParcelInDeliver parcelInDeliver = GetParcelInDeliver(parcel.Id);

            drone.Battery -= Location.Distance(drone.Location, parcelInDeliver.CollectLocation) * ElectricityConfumctiolFree;
            drone.Location = parcelInDeliver.CollectLocation;

            Dal.CollectParcel(parcel.Id);
        }
        /// <summary>
        /// rename drone
        /// </summary>
        /// <param name="droneId">drone to rename</param>
        /// <param name="model">new name for updating</param>
        public void RenameDrone(int droneId, string model)
        {
            DroneForList drone = GetDroneForList(droneId);
            drone.Model = model;


            var dlDrone = Dal.GetById<IDAL.DO.Drone>(droneId);
            dlDrone.Model = model;

            Dal.Update<IDAL.DO.Drone>(dlDrone);
        }
        /// <summary>
        /// put drone in a charge slot to charge
        /// </summary>
        /// <param name="droneId">drone to charge</param>
        public void SendDroneToCharge(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);

            if (drone.State != DroneState.Free)
            {
                throw new InValidActionException();
            }

            var availableBaseStations = Dal.GetAvailableBaseStations().Select(b => GetBaseStation(b.Id));

            BaseStation closest = drone.FindClosest(availableBaseStations);

            if (!IsEnoughBattery(drone, closest.Location))
            {
                throw new InValidActionException();
            }

            drone.Location = closest.Location;
            drone.State = DroneState.MEINTENENCE;
            drone.Battery -= ElectricityConfumctiolFree * Location.Distance(closest.Location, drone.Location);

            // What for?
            closest.EmptyChargeSlots -= 1;

            Dal.ChargeDroneAtBaseStation(drone.Id, closest.Id);
        }
        /// <summary>
        /// drone supplies its parcel at target
        /// </summary>
        /// <param name="droneId">drone which supplies</param>
        public void SupplyParcel(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);

            if (drone.DeliveredParcelId == null)
            {
                throw new InValidActionException();
            }

            var parcel = GetParcel((int)drone.DeliveredParcelId);

            if (parcel.PickedUp == null)
            {
                throw new InValidActionException();
            }

            ParcelInDeliver parcelInDeliver = GetParcelInDeliver(parcel.Id);

            if(parcelInDeliver.Position)
            {
                throw new InValidActionException();
            }

            drone.Battery -= Location.Distance(drone.Location, parcelInDeliver.Target) * GetElectricity(parcelInDeliver.Weight);
            drone.Location = parcelInDeliver.Target;
            drone.State = DroneState.Free;

            Dal.SupplyParcel(parcel.Id);
        }
        /// <summary>
        /// update base station details
        /// </summary>
        /// <param name="baseStationId">base station to update</param>
        /// <param name="name">new name</param>
        /// <param name="chargeSlots">new number of charge slots</param>
        public void UpdateBaseStation(int baseStationId, string name = null, int? chargeSlots = null)
        { 
            var station = Dal.GetById<IDAL.DO.BaseStation>(baseStationId);
            Dal.Remove<IDAL.DO.BaseStation>(station.Id);

            station.Name = name ?? station.Name;
            station.ChargeSlots = chargeSlots ?? station.ChargeSlots;

            Dal.Add(station);
        }
        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId">customer to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        public void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            var customer = Dal.GetById<IDAL.DO.Customer>(customerId);
            Dal.Remove<IDAL.DO.Customer>(customer.Id);

            customer.Name = name ?? customer.Name;
            customer.Phone = name ?? customer.Phone;

            Dal.Add(customer);
        }
        /// <summary>
        /// check weather a drone can deliver a parcel
        /// </summary>
        /// <param name="drone">deliver drone</param>
        /// <param name="parcel">delivered parcel</param>
        /// <returns></returns>
        private bool IsAbleToPassParcel(Drone drone, ParcelInDeliver parcel)
        {
            var neededBattery = Location.Distance(drone.Location, parcel.CollectLocation) * ElectricityConfumctiolFree +
                               Location.Distance(parcel.CollectLocation, parcel.Target) * GetElectricity(parcel.Weight) +
                               Location.Distance(parcel.Target, drone.FindClosest(GetAvailableBaseStations().Select(s => GetBaseStation(s.Id))).Location) * ElectricityConfumctiolFree;
           return drone.Battery >= neededBattery;
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
        /// <summary>
        /// check weather a drone has enough battery to get to location
        /// </summary>
        /// <param name="drone">drone</param>
        /// <param name="location">location to get to</param>
        /// <returns></returns>
        private bool IsEnoughBattery(DroneForList drone, Location location)
        {
            var neededBattery = Location.Distance(drone.Location, location) * ElectricityConfumctiolFree;
            return drone.Battery >= neededBattery;
        }
    }
}
