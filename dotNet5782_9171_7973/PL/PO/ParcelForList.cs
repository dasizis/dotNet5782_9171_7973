using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of parcel for list
    /// </summary>
    public class ParcelForList : PropertyChangedNotification
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

        int senderId;
        /// <summary>
        /// Name of parcel sender
        /// </summary>
        public int SenderId
        {
            get => senderId;
            set => SetField(ref senderId, value);
        }

        int targetId;
        /// <summary>
        /// Name of target sender
        /// </summary>
        public int TargetId
        {
            get => targetId;
            set => SetField(ref targetId, value);
        }

        WeightCategory weight;
        /// <summary>
        /// Parcel weight category
        /// </summary>
        public WeightCategory Weight
        {
            get => weight;
            set => SetField(ref weight, value);
        }

        Priority priority;
        /// <summary>
        /// Parcel priority
        /// </summary>
        public Priority Priority
        {
            get => priority;
            set => SetField(ref priority, value);
        }

        bool isOnWay;
        public bool IsOnWay 
        {
            get => isOnWay;
            set => SetField(ref isOnWay, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}