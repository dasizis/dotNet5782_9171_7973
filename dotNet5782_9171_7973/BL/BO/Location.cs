using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace BO
{
    public class Location
    {
        const int EARTH_RADIUS_KM = 6371;
        
        double longitude;
        [SexadecimalLongitude]
        public double Longitude 
        { 
            get => longitude;
            set
            {
                if (!Validation.IsValidLongitude(value))
                {
                    throw new ArgumentException();
                }
                longitude = value;
            }
        }
        
        double latitude;
        [SexadecimalLatitude]
        public double Latitude
        { 
            get => latitude;
            set
            {
                if (!Validation.IsValidLatitude(value))
                {
                    throw new ArgumentException();
                }
                latitude = value;
            }
        }

        public static double Distance(Location locationA, Location locationB)
        {
            static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

            double dLat = DegreesToRadians(locationA.Latitude - locationB.Latitude);
            double dLon = DegreesToRadians(locationA.Longitude - locationB.Longitude);

            double latA = DegreesToRadians(locationA.Latitude);
            double latB = DegreesToRadians(locationB.Latitude);

            var a = Math.Sin(dLat / 2) *
                    Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) *
                    Math.Sin(dLon / 2) *
                    Math.Cos(latA) *
                    Math.Cos(latB);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EARTH_RADIUS_KM * c;
        }
        public override string ToString() => this.ToStringProperties();
    }
}
