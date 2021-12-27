using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of parcel in deliver
    /// </summary>
    class ParcelInDeliver
    {
        /// <summary>
        /// Parcel Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Parcel weight category
        /// </summary>
        public WeightCategory Weight { get; set; }

        /// <summary>
        /// Parcel priority
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Determines whether parcel was picked up
        /// </summary>
        public bool WasPickedUp { get; set; }

        /// <summary>
        /// Location to collect parcel from
        /// </summary>
        public Location CollectLocation { get; set; }

        /// <summary>
        /// Location to provide parcel to
        /// </summary>
        public Location TargetLocation { get; set; }

        /// <summary>
        /// Delivery distance
        /// (distance from sender to target)
        /// </summary>
        public double DeliveryDistance { get; set; }

        /// <summary>
        /// Parcel sender
        /// </summary>
        public CustomerInDelivery Sender { get; set; }

        /// <summary>
        /// Parcel reciever
        /// </summary>
        public CustomerInDelivery Target { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
