using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;


namespace BO
{
    /// <summary>
    /// A struct to represent a PDS of customer
    /// </summary>
    public class Customer: ILocalable
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
                    throw new ArgumentException(value);
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
                    throw new ArgumentException(value);
                }
                phone = value;
            }
        }
        /// <summary>
        /// Customer location
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// List of parcels sent from customer
        /// </summary>
        public List<Parcel> Send { get; set; }
        /// <summary>
        /// List of parcels sent to customer
        /// </summary>
        public List<Parcel> Recieve { get; set; }
        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();

    }
}
