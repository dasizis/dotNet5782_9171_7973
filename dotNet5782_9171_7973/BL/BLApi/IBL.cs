using System;

namespace BLApi
{
    public interface IBL:IBLCustomer, IBLBaseStation, IBLDrone, IBLParcel
    {
        void StartDroneSimulator(int id, Action update, Func<bool> shouldStop);
    }
}