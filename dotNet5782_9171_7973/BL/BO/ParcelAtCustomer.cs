using System;
using StringUtilities;

namespace BO
{
    /// <summary>
    /// A class to represent a PDS of parcel at customer
    /// </summary>
    public class ParcelAtCustomer 
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

        ParcelState state;
        /// <summary>
        /// Parcel state
        /// </summary>
        public ParcelState State
        {
            get => state;
            set
            {
                if (!Validation.IsValidEnumOption<ParcelState>((int)value))
                {
                    throw new InvalidPropertyValueException(value);
                }
                state = value;
            }
        }

        /// <summary>
        /// The other customer related to parcel delivery
        /// (sender or reciever)
        /// </summary>
        public CustomerInDelivery OtherCustomer { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
