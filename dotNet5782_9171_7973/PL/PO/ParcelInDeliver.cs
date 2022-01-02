using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of parcel in deliver
    /// </summary>
    public class ParcelInDeliver : PropertyChangedNotification
    {
        int id;
        /// <summary>
        /// Parcel Id
        /// </summary>
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        bool wasPickedUp;
        public bool WasPickedUp
        {
            get => wasPickedUp;
            set => SetField(ref wasPickedUp, value);
        }

        double deliveryDistance;
        /// <summary>
        /// Delivery distance
        /// (distance from sender to target)
        /// </summary>
        public double DeliveryDistance
        {
            get => deliveryDistance;
            set => SetField(ref deliveryDistance, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
