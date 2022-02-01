using StringUtilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace PO
{
    class BaseStationToAdd : PropertyChangedNotification
    {
        int? id;
        /// <summary>
        /// Base Station Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int? Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        string name;
        /// <summary>
        /// Base station name
        /// </summary>       
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[a-zA-Z ]{4,14}", ErrorMessage = "Name must match a 4-14 letters only format")]
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        string longitude;
        /// <summary>
        /// Base station location longitude
        /// </summary>
        [RegularExpression(@"^-?\d+([.]\d+)?$", ErrorMessage = "Longitude must be a float number")]
        [Range(typeof(double), "-180", "180", ErrorMessage = "Longitude must be between -180 - 180")]
        [Required(ErrorMessage = "Required")]
        public string Longitude
        {
            get => longitude;
            set => SetField(ref longitude, value);
        }

        string latitude;
        /// <summary>
        /// Base station location latitude
        /// </summary>
        [RegularExpression(@"^-?(180([.]0+)?|(1[0-7]\d|\d{1,2})([.]\d+)?)$", ErrorMessage = "Longitude must be a float number in range [-180, 180]")]
        [Required(ErrorMessage = "Required")]
        public string Latitude
        {
            get => latitude;
            set => SetField(ref latitude, value);
        }

        int? chargeSlots;
        /// <summary>
        /// Base Station Id
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Charge slots number should be greater than zero")]
        public int? ChargeSlots
        {
            get => chargeSlots;
            set => SetField(ref chargeSlots, value);
        }
    }
}
