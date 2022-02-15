using System;
using BO;

namespace BLApi
{
    public interface IBL:IBLCustomer, IBLBaseStation, IBLDrone, IBLParcel
    {
        void StartDroneSimulator(int id, Action<DroneSimulatorChanges> update, Func<bool> shouldStop);
    }
}