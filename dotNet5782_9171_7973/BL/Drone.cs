using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategory MaxWeight { get; set; }
        public double BatteryState { get; set; }
        public DroneState State { get; set; }
        public ParcelInDeliver ParcelInDeliver { get; set; }
        public Location Location { get; set; }


    }
}
