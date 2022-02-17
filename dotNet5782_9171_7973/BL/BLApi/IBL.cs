using System;
using BO;

namespace BLApi
{
    public interface IBL:IBLCustomer, IBLBaseStation, IBLDrone, IBLParcel
    {
        /// <summary>
        /// Starts a new drone simulator
        /// </summary>
        /// <param name="id">The drone id</param>
        /// <param name="update">The update function</param>
        /// <param name="shouldStop">Determines weather the simulator should stop</param>
        void StartDroneSimulator(int id, Action<DroneSimulatorChanges> update, Func<bool> shouldStop);
    }
}