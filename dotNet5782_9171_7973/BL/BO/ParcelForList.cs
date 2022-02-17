using System;
using StringUtilities;

namespace BO
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

        /// <summary>
        /// Name of parcel sender
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Name of parcel target (reciever)
        /// </summary>
        public int TargetId { get; set; }

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
                    throw new InvalidPropertyValueException(value);
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
                    throw new InvalidPropertyValueException(value);
                }
                priority = value;
            }
        }

        public bool IsOnWay { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
