using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StringUtilities;


namespace PO
{
    /// <summary>
    /// A class to represent a PDS of customer
    /// </summary>
    class Customer : PropertyChangedNotification
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id { get; set; }

        string name;
        /// <summary>
        /// Customer name
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name length must be between 3-15")]
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        string phone;
        /// <summary>
        /// Customer phone number
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Phone(ErrorMessage = "Phone length must be 10")]
        public string Phone
        {
            get => phone;
            set => SetField(ref phone, value);
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
