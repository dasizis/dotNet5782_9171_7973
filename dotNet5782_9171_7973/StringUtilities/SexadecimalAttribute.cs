using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringUtilities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SexadecimalLongitudeAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SexadecimalLatitudeAttribute : Attribute
    {
    }

    static class Sexadecimal
    {
        internal static string Longitde(double longitude)
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

        internal static string Latitude(double latitude)
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
