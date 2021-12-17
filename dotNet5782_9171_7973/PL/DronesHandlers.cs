using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    static class DronesHandlers
    {
        public static event EventHandler<DroneChangedEventArg> DroneChangedEvent;
        public static void NotifyDroneChanged(object sender, int droneId)
        {
            DroneChangedEvent?.Invoke(sender, new DroneChangedEventArg(droneId));
        }

        public static event EventHandler<DroneChangedEventArg> DroneAddedEvent;
        public static void NotifyDroneAdded(object sender, int droneId)
        {
            DroneChangedEvent?.Invoke(sender, new DroneChangedEventArg(droneId));
        }

        public static BLApi.IBL bal { get; set; } = BLApi.FactoryBL.GetBL();
        public static List<BO.DroneForList> drones { get; set; } = bal.GetDronesList().ToList();

        public static void ReloadDrones()
        {
            drones = bal.GetDronesList().ToList();
        }
    }
    public class DroneChangedEventArg : EventArgs
    {
        public DroneChangedEventArg(int droneId)
        {
            DroneId = droneId;
        }
        public int DroneId { get; set; }
    }
}
