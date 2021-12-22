using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;


namespace BO
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

        WeightCategory weight;
        /// <summary>
        /// Parcel weight category
        /// </summary>
        public WeightCategory Weight
        {
            get => weight;
            set
            {
                if (!Validation.IsValidEnumOption<WeightCategory>((int)value))
                {
                    throw new ArgumentException(value.ToString());
                }
                weight = value;
            }
        }

        Priority priority;
        /// <summary>
        /// Parcel priority
        /// </summary>
        public Priority Priority
        {
            get => priority;
            set
            {
                if (!Validation.IsValidEnumOption<Priority>((int)value))
                {
                    throw new ArgumentException(value.ToString());
                }
                priority = value;
            }
        }

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
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();

    }
}
