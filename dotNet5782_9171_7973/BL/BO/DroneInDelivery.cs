using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace BO
{
    /// <summary>
    /// A class to represent a PDS of drone in delivery
    /// </summary>
    public class DroneInDelivery : ILocalable
    {
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id { get; set; }
        double batteryState;
        /// <summary>
        /// Drone battery state 
        /// (in parcents)
        /// </summary>
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
        /// <summary>
        /// Drone location
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
