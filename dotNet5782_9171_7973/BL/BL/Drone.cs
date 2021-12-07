﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    partial class BL 
    {
        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="id">the drone id</param>
        /// <param name="model">the drone model </param>
        /// <param name="maxWeight">the drone max weight to carry</param>
        /// <param name="stationId">first station for drone first charge</param>
        public void AddDrone(int id, string model, WeightCategory maxWeight, int stationId)
        {
            var station = GetBaseStation(stationId);

            var drone = new Drone()
            {
                Id = id,
                Model = model,
                MaxWeight = maxWeight,
                Battery = 0,
                Location = station.Location,
                ParcelInDeliver = null,
                State = DroneState.Meintenence,
            };

            drones.Add(
                new DroneForList()
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = drone.MaxWeight,
                    Battery = drone.Battery,
                    Location = new Location() { Latitude = drone.Location.Latitude, Longitude = drone.Location.Longitude },
                    DeliveredParcelId = null,
                    State = drone.State,
                }
            );

            try
            {
                dal.Add(new DO.Drone()
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (DO.WeightCategory)drone.MaxWeight,
                });
            }
            catch
            {
                throw new IdAlreadyExistsException(typeof(Drone), id);
            }
        }
        /// <summary>
        /// return drone list
        /// </summary>
        /// <returns>drone list</returns>
        public IEnumerable<DroneForList> GetDronesList()
        {
            return drones.Where(_ => true);
        }
        /// <summary>
        /// find a suitable parcel and assigns it to the drone
        /// </summary>
        /// <param name="droneId">drone id to assign a parcel to</param>
        public void AssignParcelToDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);

            if (drone.State == DroneState.Deliver)
            {
                throw new InValidActionException("Cannot assign parcel to busy drone.");
            }

            var parcels = dal.GetNotAssignedToDroneParcels()
                          .Where(parcel =>
                               (int)parcel.Weight < (int)drone.MaxWeight &&
                               IsAbleToPassParcel(drone, GetParcelInDeliver(parcel.Id)))
                          .OrderBy(p => p.Priority)
                          .ThenBy(p => p.Weight)
                          .ThenBy(p => Location.Distance(GetCustomer(p.SenderId).Location, drone.Location));

            if (!parcels.Any())
            {
                throw new InValidActionException("Couldn't assign any parcel to the drone.");
            }


            dal.AssignParcelToDrone(parcels.First().Id, droneId);

            drone.State = DroneState.Deliver;
        }
        /// <summary>
        /// release drone from charging
        /// </summary>
        /// <param name="droneId">drone to release</param>
        /// <param name="timeInCharge">time drone was in charge</param>
        public void FinishCharging(int droneId, double timeInCharge)
        {
            DroneForList drone = GetDroneForList(droneId);

            //TODO logic
            if (drone.State != DroneState.Meintenence)
            {
                throw new InValidActionException("Drone already in meintenece.");
            }

            drone.Battery += ChargeRate * timeInCharge;
            drone.State = DroneState.Free;

            dal.FinishCharging(drone.Id);
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

            DO.Drone dlDrone;
            try
            {
                dlDrone = dal.GetById<DO.Drone>(droneId);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Drone), droneId);
            }

            dlDrone.Model = model;

            dal.Update(dlDrone);
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
                throw new InValidActionException("Can not send a non-free drone to charge.");
            }

            var availableBaseStations = dal.GetAvailableBaseStations().Select(b => GetBaseStation(b.Id));

            BaseStation closest = drone.FindClosest(availableBaseStations);

            if (!IsEnoughBattery(drone, closest.Location))
            {
                throw new InValidActionException("Drone cannot get to base station for charging.");
            }

            drone.Location = closest.Location;
            drone.State = DroneState.Meintenence;
            drone.Battery -= ElectricityConfumctiolFree * Location.Distance(closest.Location, drone.Location);

            // What for?
            closest.EmptyChargeSlots -= 1;

            dal.ChargeDroneAtBaseStation(drone.Id, closest.Id);
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
                               Location.Distance(parcel.CollectLocation, parcel.TargetLocation) * GetElectricity(parcel.Weight) +
                               Location.Distance(parcel.TargetLocation, drone.FindClosest(GetAvailableBaseStations().Select(s => GetBaseStation(s.Id))).Location) * ElectricityConfumctiolFree;
            return drone.Battery >= neededBattery;
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