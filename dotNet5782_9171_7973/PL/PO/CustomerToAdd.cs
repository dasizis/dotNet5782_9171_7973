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
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Name length must be between 3-10")]
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


        Location location;
        /// <summary>
        /// Customer location
        /// </summary>       
        [Required(ErrorMessage = "Required")]
        public Location Location
        {
            get => location;
            set => SetField(ref location, value);
        }

    }
}
