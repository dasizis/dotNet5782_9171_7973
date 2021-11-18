using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public static class Localable
    {
        const int EARTH_RADIUS_KM = 6371;


        //public static double Distance(Locat localableA, T localableB): 
        //{
        //    static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

        //    double dLat = DegreesToRadians(localableA.Location.Latitude - localableB.Location.Latitude);
        //    double dLon = DegreesToRadians(localableA.Location.Longitude - localableB.Location.Longitude);

        //    double latA = DegreesToRadians(localableA.Location.Latitude);
        //    double latB = DegreesToRadians(localableB.Location.Latitude);

        //    var a = Math.Sin(dLat / 2) * 
        //            Math.Sin(dLat / 2) +
        //            Math.Sin(dLon / 2) * 
        //            Math.Sin(dLon / 2) * 
        //            Math.Cos(latA) * 
        //            Math.Cos(latB);

        //    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        //    return EARTH_RADIUS_KM * c;
        //}

        public static T FindClosest<T>(this ILocalable location,IEnumerable<T> localables) where T: ILocalable
        {
            return localables.OrderBy(l => Location.Distance(location.Location, l.Location)).First();
        }

        public static Location FindClosest<T>(this Location location, IEnumerable<T> localables) where T : ILocalable
        {
            return localables.OrderBy(l => Location.Distance(location, l.Location)).First().Location;
        }

        public static Location FindClosest(this Location location, IEnumerable<Location> locations)
        {
            return locations.OrderBy(l => Location.Distance(location, l)).First();
        }
    }
}
