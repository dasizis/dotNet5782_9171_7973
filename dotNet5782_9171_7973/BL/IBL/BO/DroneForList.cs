using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
{
    public class DroneForList : ILocalable
    {

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategory MaxWeight { get; set; }
        public double Battery { get; set; }

        private DroneState state;
        public DroneState State { get => state; set 
            { state = value; if (this.Id == 90 && value == DroneState.Deliver) throw new Exception("State"); } 
        }
        public Location Location { get; set; }
        public int? DeliveredParcelId { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
