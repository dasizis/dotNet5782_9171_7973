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
        [RegularExpression(@"[a-zA-Z ]{4,14}", ErrorMessage = "Name must match a 4-14 letters only format")]
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
        [RegularExpression(@"^0\d{9}", ErrorMessage = "Phone must match a 10 digits begins with 0 format")]
        public string Phone
        {
            get => phone;
            set => SetField(ref phone, value);
        }


        string longitude;
        /// <summary>
        /// Customer location longitude
        /// </summary>
        [RegularExpression(@"^-?(180([.]0+)?|(1[0-7]\d|\d{1,2})([.]\d+)?)$", ErrorMessage = "Longitude must be a float number in range [-180, 180]")]
        [Required(ErrorMessage = "Required")]
        public string Longitude
        {
            get => longitude;
            set => SetField(ref longitude, value);
        }

        string latitude;
        /// <summary>
        /// Customer location latitude
        /// </summary>
        [RegularExpression(@"^-?(90([.]0+)?|[1-8]?\d([.]\d+)?)$", ErrorMessage = "Latitude must be a float number in range [-90, 90]")]
        [Required(ErrorMessage = "Required")]
        public string Latitude
        {
            get => latitude;
            set => SetField(ref latitude, value);
        }
    }
}
