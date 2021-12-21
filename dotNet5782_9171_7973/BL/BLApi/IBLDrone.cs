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
        void AddDrone(int id, string model, WeightCategory maxWeight, int stationId);

        void RenameDrone(int droneId, string newName);

        void ChargeDrone(int droneId);

        void FinishCharging(int droneId);

        IEnumerable<DroneForList> GetDronesList();

        Drone GetDrone(int id);

        int GetDroneBaseStation(int droneId);


        IEnumerable<DroneForList> GetFilteredDronesList(int? stateOption, int? weightOption);

    }
}
