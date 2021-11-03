using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneForList
    { 
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategory MaxWeight { get; set; }
        public double BatteryState { get; set; }
        public DroneState DroneState { get; set; }
        public Location CurrentLocation { get; set; }
        public int DeliveredParcelId { get; set; }
    }
}
