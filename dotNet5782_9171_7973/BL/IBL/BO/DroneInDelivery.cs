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
        public double BatteryState { get; set; }
        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                if (!Validation.IsValidLatitude(value.Latitude)
                    || !Validation.IsValidLatitude(value.Longitude))
                {
                    throw new ArgumentException(value.ToString());
                }
                location = value;
            }
        }
        public override string ToString() => this.ToStringProps();
    }
}
