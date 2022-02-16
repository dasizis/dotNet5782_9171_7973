using System;
using System.Collections.Generic;
using System.Linq;

namespace BO
{
    public static class Localable
    {
        const int EARTH_RADIUS_KM = 6371;

        /// <summary>
        /// Calculate distance between two locations
        /// </summary>
        /// <param name="locationA">first location</param>
        /// <param name="locationB">second location</param>
        /// <returns>the distance</returns>
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

        /// <summary>
        /// find the location from a list of localables 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location">localable starting point location</param>
        /// <param name="localables">list of localables to find from</param>
        /// <returns>closest location to the starting point or <c>default(T)</c> if the locables list is empty</returns>
        public static T FindClosest<T>(this ILocalable location,IEnumerable<T> localables) where T: ILocalable
        {
            if (!localables.Any()) return default;
            return localables.OrderBy(l => Distance(location.Location, l.Location)).First();
        }

        /// <summary>
        /// Find the sclosest localable from a list of localables 
        /// </summary>
        /// <typeparam name="T">type of localables</typeparam>
        /// <param name="location">location starting point location</param>
        /// <param name="localables">list of localables to search in</param>
        /// <returns>closest location to the starting point</returns>
        public static Location FindClosest<T>(this Location location, IEnumerable<T> localables) where T : ILocalable
        {
            return localables.OrderBy(l => Distance(location, l.Location)).First().Location;
        }

        /// <summary>
        /// Finds the closest localable from a list of localables 
        /// </summary>
        /// <typeparam name="T">type of localables</typeparam>
        /// <param name="location">location starting point location</param>
        /// <param name="localables">list of localables to search in</param>
        /// <returns>closest location to the starting point</returns>
        public static Location FindClosest(this Location location, IEnumerable<Location> locations)
        {
            return locations.OrderBy(l => Distance(location, l)).First();
        }
    }
}
