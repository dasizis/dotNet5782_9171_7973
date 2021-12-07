using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
{
    public class DroneInDelivery : ILocalable
    {
        public int Id { get; set; }
        double batteryState;
        public double BatteryState
        {
            get => batteryState;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                batteryState = value;
            }
        }
        public Location Location { get; set; }
        public override string ToString() => this.ToStringProperties();
    }
}
