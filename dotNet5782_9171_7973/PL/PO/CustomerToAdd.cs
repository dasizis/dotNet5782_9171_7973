using StringUtilities;
using System.ComponentModel.DataAnnotations;

namespace PO
{
    
    //public void AddCustomer(int id, string name, string phone, Location location)

    class CustomerToAdd : PropertyChangedNotification
    {
        int? id;
        /// <summary>
        /// Customer Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int? Id
        {
            get => id;
            set => SetField(ref id, value);
        }

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
        /// Customer name
        /// </summary>       
        [Required(ErrorMessage = "Required")]
        [Phone(ErrorMessage = "Phone length must be 10")]
        public string Phone
        {
            get => phone;
            set => SetField(ref phone, value);
        }


        double? longitude;
        /// <summary>
        /// Customer location longitude
        /// </summary>
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 - 180")]
        [Required(ErrorMessage = "Required")]
        public double? Longitude
        {
            get => longitude;
            set => SetField(ref longitude, value);
        }

        double? latitude;
        /// <summary>
        /// Customer location latitude
        /// </summary>
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 - 90")]
        [Required(ErrorMessage = "Required")]
        public double? Latitude
        {
            get => latitude;
            set => SetField(ref latitude, value);
        }
    }
}
