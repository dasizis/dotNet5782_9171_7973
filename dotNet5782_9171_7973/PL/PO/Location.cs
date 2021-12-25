using System;
using System.ComponentModel.DataAnnotations;
using StringUtilities;

namespace PO
{
    /// <summary>
    /// A class to represent a location
    /// </summary>
    class Location : PropertyChangedNotification
    {
        double? longitude;
        /// <summary>
        /// Location longitude
        /// </summary>
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 - 180")]
        [SexadecimalLongitude]
        [Required(ErrorMessage = "Required")]
        public double? Longitude
        {
            get => longitude;
            set => SetField(ref longitude, value);
        }

        double? latitude;
        /// <summary>
        /// Location latitude
        /// </summary>
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 - 90")]
        [SexadecimalLatitude]
        [Required(ErrorMessage = "Required")]
        public double? Latitude
        {
            get => latitude;
            set => SetField(ref latitude, value);
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
