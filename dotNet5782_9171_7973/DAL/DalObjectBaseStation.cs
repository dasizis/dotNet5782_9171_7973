using System;
using System.Collections.Generic;
using IDAL.DO;


namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// add a base station to base stations list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="chargeSlots"></param>
        public void AddBaseStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            if (DataSource.baseStations.DoesIdExist(id))
            {
                throw new IdAlreadyExistsException(typeof(BaseStation), id);
            }

            BaseStation baseStation = new BaseStation
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = chargeSlots
            };

            DataSource.baseStations.Add(baseStation);
        }

        /// <summary>
        /// sends drone to charge
        /// </summary>
        /// <param name="droneId"></param>
        public void ChargeDroneAtBaseStation(int droneId)
        {
            Drone drone = (Drone)DataSource.drones.GetById(droneId);

            drone.Status = DroneStatus.Meintenence;
            DroneCharge droneCharge = new DroneCharge();

            // find a base station where there is an available charge slot
            BaseStation baseStation = GetStationsWithEmptySlots()[0];

            droneCharge.StationId = baseStation.Id;
            droneCharge.DroneId = drone.Id;

            DataSource.droneCharges.Add(droneCharge);
        }

        /// <summary>
        /// returns the base stations list as array
        /// </summary>
        /// <returns>an array of all the exist base stations</returns>
        public IEnumerable<BaseStation> GetBaseStationList()
        {
            return DataSource.baseStations;
        }

        /// <summary>
        /// finds all station with available charge slots
        /// </summary>
        /// <returns>the array of stations found</returns>
        public IEnumerable<BaseStation> GetStationsWithEmptySlots()
        {
            // Find all base stations where there are available charge slots
            return DataSource.baseStations.FindAll(
                b => b.ChargeSlots > DataSource.droneCharges.FindAll(
                    d => d.StationId == b.Id).Count);
        }
    }
}
