using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IBLDrone
    {
        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="id">the drone id</param>
        /// <param name="model">the drone model </param>
        /// <param name="maxWeight">the drone max weight to carry</param>
        /// <param name="stationId">first station for drone first charge</param>
        void AddDrone(int id, string model, WeightCategory maxWeight, int stationId);
        Drone GetDrone(int id);
        IEnumerable<DroneForList> GetDronesList();
        IEnumerable<DroneForList> GetFilteredDronesList(int? stateOption, int? weightOption);
        int GetDroneBaseStation(int droneId);

        void RenameDrone(int droneId, string newName);

        void ChargeDrone(int droneId);

        void FinishCharging(int droneId);

        /// <summary>
        /// Deletes a drone
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        void DeleteDrone(int droneId);
    }
}
