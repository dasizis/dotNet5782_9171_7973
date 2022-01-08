using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    partial class BL 
    {
        public void AddDrone(int id, string model, WeightCategory maxWeight, int stationId)
        {
            var drone = new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                Battery = 0,
                Location = GetBaseStation(stationId).Location,
                ParcelInDeliver = null,
                State = DroneState.Maintenance,
            };

            try
            {
                dal.Add(new DO.Drone()
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (DO.WeightCategory)drone.MaxWeight, 
                });
            }
            catch (DO.IdAlreadyExistsException)
            {
                throw new IdAlreadyExistsException(typeof(Drone), id);
            }

            drones.Add(
                new DroneForList()
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = drone.MaxWeight,
                    Battery = drone.Battery,
                    Location = drone.Location.Clone(),
                    DeliveredParcelId = null,
                    State = drone.State,
                }
            );

            dal.AddDroneCharge(drone.Id, stationId);
        }

        public Drone GetDrone(int id)
        {
            DroneForList drone = drones.FirstOrDefault(d => d.Id == id);

            if (drone == null)
                throw new ObjectNotFoundException(typeof(Drone));

            ParcelInDeliver parcelInDeliver = drone.State == DroneState.Deliver 
                                              ? GetParcelInDeliver((int)drone.DeliveredParcelId) 
                                              : null;
            return new Drone()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = drone.Location.Clone(),
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                State = drone.State,
                ParcelInDeliver = parcelInDeliver,
            };
        }

        public IEnumerable<DroneForList> GetDronesList()
        {
            return drones.Select(drone => drone.Clone());
        }

        public IEnumerable<DroneForList> GetFilteredDronesList(int? stateOption, int? weightOption)
        {
            return GetDronesList().Where(d => (stateOption == null || d.State == (DroneState)stateOption) && 
                                              (weightOption == null || d.MaxWeight == (WeightCategory)weightOption));
        }

        public int GetDroneBaseStation(int droneId)
        {
            try
            {
                return dal.GetSingle<DO.DroneCharge>(c => c.DroneId == droneId).StationId;
            }
            catch (DO.ObjectNotFoundException)
            {
                throw new ObjectNotFoundException("Drone is not being charged");
            }
        }

        public void RenameDrone(int droneId, string model)
        {
            DroneForList drone = GetDroneForListRef(droneId);
            drone.Model = model;

            try
            {
                dal.Update<DO.Drone>(droneId, nameof(drone.Model), model);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Drone), e);
            }
        }

        public void ChargeDrone(int droneId)
        {
            DroneForList drone = GetDroneForListRef(droneId);

            if (drone.State != DroneState.Free)
            {
                throw new InvalidActionException("Can not send a non-free drone to charge.");
            }

            var availableBaseStations = GetAvailableBaseStations().Select(b => GetBaseStation(b.Id));
            if (!availableBaseStations.Any())
            {
                throw new InvalidActionException("There is no empty base station");
            }

            BaseStation closest = drone.FindClosest(availableBaseStations);

            if (!IsEnoughBattery(drone, closest.Location))
            {
                throw new InvalidActionException("Drone cannot get to base station for charging.");
            }

            drone.Location = closest.Location;
            drone.State = DroneState.Maintenance;
            drone.Battery -= ElectricityConfumctiolFree * Localable.Distance(closest.Location, drone.Location);

            dal.AddDroneCharge(drone.Id, closest.Id);
        }
        
        public void FinishCharging(int droneId)
        {
            DroneForList drone = GetDroneForListRef(droneId);

            if (drone.State != DroneState.Maintenance)
            {
                throw new InvalidActionException("Drone is not in meintenece");
            }
 
            var dalCharge = dal.GetFilteredList<DO.DroneCharge>(c => c.DroneId == droneId).FirstOrDefault();
            if (dalCharge.Equals(default(DO.DroneCharge)))
            {
                throw new InvalidActionException("Drone is not being charged");
            }

            drone.Battery = Math.Min(drone.Battery + ChargeRate * (DateTime.Now - dalCharge.StartTime).TotalHours,
                                     MAX_CHARGE);
            drone.State = DroneState.Free;

            dal.DeleteWhere<DO.DroneCharge>(charge => charge.DroneId == drone.Id);
        }

        public void AssignParcelToDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);

            if (drone.State == DroneState.Deliver)
            {
                throw new InvalidActionException("Cannot assign parcel to busy drone.");
            }

            var parcels = GetNotAssignedToDroneParcels().Select(parcel => GetParcel(parcel.Id));

            var orderedParcels = from parcel in parcels
                                 where parcel.Weight < drone.MaxWeight
                                       && IsAbleToDeliverParcel(drone, GetParcelInDeliver(parcel.Id))
                                 orderby parcel.Priority, parcel.Weight, Localable.Distance(GetCustomer(parcel.Sender.Id).Location, drone.Location)
                                 select parcel;

            if (!parcels.Any())
            {
                throw new InvalidActionException("Couldn't assign any parcel to the drone.");
            }

            Parcel selectedParcel = parcels.First();
            dal.Update<DO.Parcel>(selectedParcel.Id, nameof(DO.Parcel.DroneId), droneId);
            dal.Update<DO.Parcel>(selectedParcel.Id, nameof(DO.Parcel.Scheduled), DateTime.Now);

            var droneForList = GetDroneForListRef(droneId);
            droneForList.State = DroneState.Deliver;
            droneForList.DeliveredParcelId = selectedParcel.Id;
        }

        public void PickUpParcel(int droneId)
        {
            DroneForList drone = GetDroneForListRef(droneId);

            // Does the drone have a parcel?
            if (drone.DeliveredParcelId == null)
            {
                throw new InvalidActionException("No parcel is assigned to drone.");
            }

            DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<DO.Parcel>((int)drone.DeliveredParcelId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Parcel), e);
            }

            // Was the parcel collected?
            if (parcel.PickedUp != null)
            {
                throw new InvalidActionException("Parcel assigned to drone was already picked.");
            }

            ParcelInDeliver parcelInDeliver = GetParcelInDeliver(parcel.Id);
            dal.Update<DO.Parcel>(parcel.Id, nameof(parcel.PickedUp), DateTime.Now);

            drone.Battery -= Localable.Distance(drone.Location, parcelInDeliver.CollectLocation) * ElectricityConfumctiolFree * 0.1;
            drone.Location = parcelInDeliver.CollectLocation;
        }

        public void DeleteDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);

            if (drone.State != DroneState.Free)
                throw new InvalidActionException("Can not delete a non free drone");

            drones.RemoveAll(drone => drone.Id == droneId);

            dal.Delete<DO.Drone>(droneId);
        }

        #region Helpers

        /// <summary>
        /// Checks weather a drone can deliver a parcel
        /// </summary>
        /// <param name="drone">The deliver drone</param>
        /// <param name="parcel">The delivered parcel</param>
        /// <returns>true if the drone can deliver the parcel otherwise false</returns>
        private bool IsAbleToDeliverParcel(Drone drone, ParcelInDeliver parcel)
        {
            var neededBattery = Localable.Distance(drone.Location, parcel.CollectLocation) * ElectricityConfumctiolFree +
                                Localable.Distance(parcel.CollectLocation, parcel.TargetLocation) * GetElectricity(parcel.Weight) +
                                Localable.Distance(parcel.TargetLocation, drone.FindClosest(GetAvailableBaseStations().Select(s => GetBaseStation(s.Id))).Location) * ElectricityConfumctiolFree;
            return drone.Battery >= neededBattery;
        }

        /// <summary>
        /// Checks weather a drone has enough battery to get to location
        /// </summary>
        /// <param name="drone">drone</param>
        /// <param name="location">location to get to</param>
        /// <returns>true if the drone can get the location otherwise false</returns>
        private bool IsEnoughBattery(DroneForList drone, Location location)
        {
            var neededBattery = Localable.Distance(drone.Location, location) * ElectricityConfumctiolFree;
            return drone.Battery >= neededBattery;
        }

        /// <summary>
        /// Returns a refrence to the drone in the BL drones list
        /// </summary>
        /// <param name="id">The drone Id</param>
        /// <returns>A refrence to <see cref="DroneForList"/> from BL drones list</returns>
        internal DroneForList GetDroneForListRef(int id)
        {
            var drone = drones.FirstOrDefault(d => d.Id == id);

            if (drone == default)
            {
                throw new ObjectNotFoundException(typeof(DroneForList));
            }

            return drone;
        }

        /// <summary>
        /// Returns a converted drone to drone for list
        /// </summary>
        /// <param name="id">The id of requested drone</param>
        /// <returns>A clone of <see cref="DroneForList"/></returns>
        /// <exception cref="ObjectNotFoundException" />
        internal DroneForList GetDroneForList(int id)
        {
            return GetDroneForListRef(id).Clone();
        }

        /// <summary>
        /// Returns a converted drone to drone in charge
        /// </summary>
        /// <param name="id">The drone Id</param>
        /// <returns>A <see cref="DroneInCharge"/></returns>
        internal DroneInCharge GetDroneInCharge(int id)
        {
            return new DroneInCharge()
            {
                Id = id,
                Battery = GetDroneForList(id).Battery,
            };
        }

        /// <summary>
        /// Returns a converted drone to drone in delivery
        /// </summary>
        /// <param name="id">The drone Id</param>
        /// <returns>A <see cref="DroneInDelivery"/></returns>
        internal DroneInDelivery GetDroneInDelivery(int id)
        {
            var drone = GetDrone(id);

            return new DroneInDelivery()
            {
                Id = id,
                Battery = drone.Battery,
                Location = drone.Location,
            };
        }

        #endregion
    }
}
