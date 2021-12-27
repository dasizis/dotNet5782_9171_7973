using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of parcel for list
    /// </summary>
    public class ParcelForList
    {
        /// <summary>
        /// Parcel Id
        /// </summary>
        public int Id { get; set; }

        string senderName;
        /// <summary>
        /// Name of parcel sender
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Name of target sender
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Parcel weight category
        /// </summary>
        public WeightCategory Weight { get; set; }

        /// <summary>
        /// Parcel priority
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}