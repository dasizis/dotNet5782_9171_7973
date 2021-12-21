using System;
using StringUtilities;

namespace DO
{
    /// <summary>
    /// A struct to represent a PDS of parcel
    /// </summary>
    public struct Parcel : IIdentifiable, IDeletable
    {
        /// <summary>
        /// The Id number of the drone that delivers the parcel (if exists)
        /// </summary>
        public int? DroneId { get; set; }

        /// <summary>
        /// Parcel Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sender customer Id
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Target customer Id
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// Parcel weight category
        /// </summary>
        public WeightCategory Weight { get; set; }

        /// <summary>
        /// Parcel priority
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Parcel requested time (If has already occurred)
        /// </summary>
        public DateTime? Requested { get; set; }

        /// <summary>
        /// Parcel scheduled time, the time the parcel was associated with drone
        /// (If has already occurred)
        /// </summary>
        public DateTime? Scheduled { get; set; }

        /// <summary>
        /// Parcel picked up time, the time the parcel was picked up by a drone
        /// (If has already occurred)
        /// </summary>
        public DateTime? PickedUp { get; set; }

        /// <summary>
        /// Parcel supplied time, the time the parcel was supplied to the target customer up
        /// (If has already occurred)
        /// </summary>
        public DateTime? Supplied { get; set; }

        /// <summary>
        /// Determines whether the parcel is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of parcel</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
