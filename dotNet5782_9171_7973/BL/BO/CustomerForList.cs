using System;
using StringUtilities;

namespace BO
{
    /// <summary>
    /// A class to represent a PDS of customer for list
    /// </summary>
    public class CustomerForList 
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id { get; set; }

        string name;
        /// <summary>
        /// Customer name
        /// </summary>
        public string Name 
        { 
            get => name;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new InvalidPropertyValueException(value);
                }
                name = value;
            }
        }

        string phone;
        /// <summary>
        /// Customer phone number
        /// </summary>
        public string Phone
        {
            get => phone;
            set
            {
                if (!Validation.IsValidPhone(value))
                {
                    throw new InvalidPropertyValueException(value);
                }
                phone = value;
            }
        }

        int parcelsSendAndSupplied;
        /// <summary>
        /// Number of parcels sent by customer and supplied
        /// </summary>
        public int ParcelsSendAndSupplied
        {
            get => parcelsSendAndSupplied;
            set
            {
                if (value < 0)
                {
                    throw new InvalidPropertyValueException(value);
                }
                parcelsSendAndSupplied = value;
            }
        }

        int parcelsSendAndNotSupplied;
        /// <summary>
        /// Number of parcels sent by customer and were not supplied
        /// </summary>
        public int ParcelsSendAndNotSupplied
        {
            get => parcelsSendAndNotSupplied;
            set
            {
                if (value < 0)
                {
                    throw new InvalidPropertyValueException(value);
                }
                parcelsSendAndNotSupplied = value;
            }
        }

        int parcelsRecieved;
        /// <summary>
        /// Number of parcels customer recieved
        /// </summary>
        public int ParcelsRecieved
        {
            get => parcelsRecieved;
            set
            {
                if (value < 0)
                {
                    throw new InvalidPropertyValueException(value);
                }
                parcelsRecieved = value;
            }
        }

        int parcelsOnWay;
        /// <summary>
        /// Number of parcels on the way to customer
        /// </summary>
        public int ParcelsOnWay
        {
            get => parcelsOnWay;
            set
            {
                if (value < 0)
                {
                    throw new InvalidPropertyValueException(value);
                }
                parcelsOnWay = value;
            }
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
