using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class DroneSimulator
    {
        private DroneForList drone;

        private BL bl;

        public DroneSimulator(int id, Action updateAction, Func<bool> shouldStop, double delay = 500)
        {
            drone = bl.GetDroneForListRef(id);

            while (!shouldStop())
            {
                switch (drone.State)
                {
                    case DroneState.Free:
                        HandleFreeState(drone, updateAction, shouldStop);
                        break;

                    case DroneState.Maintenance:
                        HandleMaintenanceState(drone, updateAction, shouldStop);
                        break;

                    case DroneState.Deliver:
                        HandleDeliverState(drone, updateAction, shouldStop);
                        break;
                }
            }
        }

        private void HandleDeliverState(DroneForList drone, Action updateAction, Func<bool> shouldStop)
        {

            throw new NotImplementedException();
        }

        private void HandleMaintenanceState(DroneForList drone, Action updateAction, Func<bool> shouldStop)
        {
            throw new NotImplementedException();
        }

        private void HandleFreeState(DroneForList drone, Action updateAction, Func<bool> shouldStop)
        {
            throw new NotImplementedException();
        }
    }
}
