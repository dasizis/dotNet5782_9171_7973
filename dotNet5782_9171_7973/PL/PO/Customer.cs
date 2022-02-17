using StringUtilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PO
{
    /// <summary>
    /// A class to represent a PDS of customer
    /// </summary>
    public class Customer : PropertyChangedNotification, ILocalable, IIdentifiable
    {
        int id;
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id 
        {
            get => id;
            set => SetField(ref id, value);
        }

        string name;
        /// <summary>
        /// Customer name
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[a-zA-Z ]{4,14}", ErrorMessage = "Name must match a 4-14 letters only format")]
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
        [RegularExpression(@"^0[0-9]{9}", ErrorMessage = "Phone must match a 10 digits begins with 0 format")]
        public string Phone
        {
            get => phone;
            set => SetField(ref phone, value);
        }

        string mail;
        /// <summary>
        /// Customer mail address
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\w{4,}@gmail[.]com$", ErrorMessage = "Mail must match a Gmail address format")]
        public string Mail
        {
            get => mail;
            set => SetField(ref mail, value);
        }

        Location location;
        /// <summary>
        /// Customer location
        /// </summary>
        public Location Location
        {
            get => location;
            set => SetField(ref location, value);
        }

        List<Parcel> send;
        /// <summary>
        /// List of parcels sent from customer
        /// </summary>
        public List<Parcel> Send
        {
            get => send;
            set => SetField(ref send, value);
        }

        List<Parcel> recieve;
        /// <summary>
        /// List of parcels sent to customer
        /// </summary>
        public List<Parcel> Recieve
        {
            get => recieve;
            set => SetField(ref recieve, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();

    }
}
