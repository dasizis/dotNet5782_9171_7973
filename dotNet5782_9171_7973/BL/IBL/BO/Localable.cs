using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public static class Localable
    {


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

        /// <summary>
        /// find the location from a list of localables 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location">localable starting point location</param>
        /// <param name="localables">list of localables to find from</param>
        /// <returns>closest location to the starting point</returns>
        public static T FindClosest<T>(this ILocalable location,IEnumerable<T> localables) where T: ILocalable
        {
            return localables.OrderBy(l => Location.Distance(location.Location, l.Location)).First();
        }
        /// <summary>
        /// find the closest localable from a list of localables 
        /// </summary>
        /// <typeparam name="T">type of localables</typeparam>
        /// <param name="location">location starting point location</param>
        /// <param name="localables">list of localables to search in</param>
        /// <returns>closest location to the starting point</returns>
        public static Location FindClosest<T>(this Location location, IEnumerable<T> localables) where T : ILocalable
        {
            return localables.OrderBy(l => Location.Distance(location, l.Location)).First().Location;
        }
        /// <summary>
        /// find the closest localable from a list of localables 
        /// </summary>
        /// <typeparam name="T">type of localables</typeparam>
        /// <param name="location">location starting point location</param>
        /// <param name="localables">list of localables to search in</param>
        /// <returns>closest location to the starting point</returns>
        public static Location FindClosest(this Location location, IEnumerable<Location> locations)
        {
            return locations.OrderBy(l => Location.Distance(location, l)).First();
        }
    }
}
