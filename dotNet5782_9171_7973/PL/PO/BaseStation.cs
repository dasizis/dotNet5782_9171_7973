using StringUtilities;
using System.Collections.Generic;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of base station
    /// </summary>
    class BaseStation : PropertyChangedNotification
    {
        /// <summary>
        /// Base station Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Base station name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base station location
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Number of empty charge slots in base station
        /// </summary>
        public int EmptyChargeSlots { get; set; }

        /// <summary>
        /// List of drones beeing in charged at base station
        /// </summary>
        public List<Drone> DronesInCharge { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
