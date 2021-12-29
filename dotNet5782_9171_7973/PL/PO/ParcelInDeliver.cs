using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of parcel in deliver
    /// </summary>
    public class ParcelInDeliver
    {
        /// <summary>
        /// Parcel Id
        /// </summary>
        public int Id { get; set; }

        public bool WasPickedUp { get; set; }

        /// <summary>
        /// Delivery distance
        /// (distance from sender to target)
        /// </summary>
        public double DeliveryDistance { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
