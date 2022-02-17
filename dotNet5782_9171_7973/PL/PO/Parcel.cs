using StringUtilities;
using System;

namespace PO
{
    /// <summary>
    /// A class to represent a PDS of parcel
    /// </summary>
    public class Parcel : PropertyChangedNotification, IIdentifiable
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

        CustomerInDelivery sender;
        /// <summary>
        /// Parcel sender
        /// </summary>
        public CustomerInDelivery Sender
        {
            get => sender;
            set => SetField(ref sender, value);
        }

        CustomerInDelivery target;
        /// <summary>
        /// Parcel reciever
        /// </summary>
        public CustomerInDelivery Target
        {
            get => target;
            set => SetField(ref target, value);
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

        int? droneId;
        /// <summary>
        /// Drone which delivers parcel
        /// </summary>
        public int? DroneId
        {
            get => droneId;
            set => SetField(ref droneId, value);
        }

        DateTime? requested;
        /// <summary>
        /// Parcel requested time (If has already occurred)
        /// </summary>
        public DateTime? Requested
        {
            get => requested;
            set => SetField(ref requested, value);
        }

        DateTime? scheduled;
        /// <summary>
        /// Parcel scheduled time, the time the parcel was associated with drone
        /// (If has already occurred)
        /// </summary>
        public DateTime? Scheduled
        {
            get => scheduled;
            set => SetField(ref scheduled, value);
        }

        DateTime? pickedUp;
        /// <summary>
        /// Parcel picked up time, the time the parcel was picked up by a drone
        /// (If has already occurred)
        /// </summary>
        public DateTime? PickedUp
        {
            get => pickedUp;
            set => SetField(ref pickedUp, value);
        }

        DateTime? supplied;
        /// <summary>
        /// Parcel supplied time, the time the parcel was supplied to the target customer up
        /// (If has already occurred)
        /// </summary>
        public DateTime? Supplied
        {
            get => supplied;
            set => SetField(ref supplied, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
