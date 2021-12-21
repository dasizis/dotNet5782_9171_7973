using System;
using StringUtilities;

namespace DO
{
    /// <summary>
    /// A struct to represent a PDS of drone charge 
    /// (An entity to describe the relation: "drone is loaded at base station")
    /// </summary>
    public struct DroneCharge: IDeletable
    {
        /// <summary>
        /// Base station Id
        /// </summary>
        public int StationId { get; set; }

        /// <summary>
        /// Loaded drone Id
        /// </summary>
        public int DroneId { get; set; }

        /// <summary>
        /// Charging start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Determines whether the drone charge is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of drone charge</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
