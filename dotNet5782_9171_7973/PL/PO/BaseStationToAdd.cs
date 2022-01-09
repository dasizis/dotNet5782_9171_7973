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
        [StringLength(14, MinimumLength = 4, ErrorMessage = "Name length must be between 4-14")]
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        double? longitude;
        /// <summary>
        /// Base station location longitude
        /// </summary>
        [SexadecimalLongitude]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 - 180")]
        [Required(ErrorMessage = "Required")]
        public double? Longitude
        {
            get => longitude;
            set => SetField(ref longitude, value);
        }

        double? latitude;
        /// <summary>
        /// Base station location latitude
        /// </summary>
        [SexadecimalLatitude]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 - 90")]
        [Required(ErrorMessage = "Required")]
        public double? Latitude
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
