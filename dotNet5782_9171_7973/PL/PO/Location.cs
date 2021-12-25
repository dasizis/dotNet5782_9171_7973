using System;
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
        [SexadecimalLongitude]
        public double? Longitude
        {
            get => longitude;
            set => SetField(ref longitude, value);
        }

        double? latitude;
        /// <summary>
        /// Location latitude
        /// </summary>
        [SexadecimalLatitude]
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
