using System;
using StringUtilities;

namespace BO
{
    /// <summary>
    /// A class to represent a location
    /// </summary>
    public class Location
    {        
        double longitude;
        /// <summary>
        /// Location longitude
        /// </summary>
        [SexadecimalLongitude]
        public double Longitude 
        { 
            get => longitude;
            set
            {
                if (!Validation.IsValidLongitude(value))
                {
                    throw new ArgumentException("Longitude value is not valid.");
                }
                longitude = value;
            }
        }
        
        double latitude;
        /// <summary>
        /// Location latitude
        /// </summary>
        [SexadecimalLatitude]
        public double Latitude
        { 
            get => latitude;
            set
            {
                if (!Validation.IsValidLatitude(value))
                {
                    throw new ArgumentException("Latitude value is not valid.");
                }
                latitude = value;
            }
        }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <c>ToString()</c> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
