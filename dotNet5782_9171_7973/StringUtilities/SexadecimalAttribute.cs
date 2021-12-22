using System;

namespace StringUtilities
{

    /// <summary>
    /// An Attribute to mark that the following property is a longitude property
    /// and should be displyed in a sexadecimal format
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SexadecimalLongitudeAttribute : Attribute
    {
    }

    /// <summary>
    /// An Attribute to mark that the following property is a latitude property
    /// and should be displyed in a sexadecimal format
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SexadecimalLatitudeAttribute : Attribute
    {
    }

    /// <summary>
    /// Static helper methods for sexadecimal attributes
    /// </summary>
    public static class Sexadecimal
    {
        /// <summary>
        /// Converts a longitude value to sexadecimal representation
        /// </summary>
        /// <param name="longitude">The longitude value</param>
        /// <returns>The longitde in a sexadecimal <see cref="string"/> representation</returns>
        public static string Longitde(double longitude)
        {
            string ch = "E";
            if (longitude < 0)
            {
                ch = "W";
                longitude = -longitude;
            }

            int deg = (int)longitude;
            int min = (int)(60 * (longitude - deg));
            double sec = (longitude - deg) * 3600 - min * 60;
            return $"{deg}° {min}′ {sec}″ {ch}";

        }

        /// <summary>
        /// Converts a latitude value to sexadecimal representation
        /// </summary>
        /// <param name="latitude">The latitude value</param>
        /// <returns>The latitude in a sexadecimal <see cref="string"/> representation</returns>
        public static string Latitude(double latitude)
        {
            string ch = "N";
            if (latitude < 0)
            {
                ch = "S";
                latitude = -latitude;
            }
            int deg = (int)latitude;
            int min = (int)(60 * (latitude - deg));
            double sec = (latitude - deg) * 3600 - min * 60;
            return $"{deg}° {min}′ {sec}″ {ch}";
        }
    }
}
