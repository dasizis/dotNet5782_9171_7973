using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace PL
{
    static class Validation
    {
        internal static bool IsValidPhone(string phone)
        {
            foreach (char ch in phone)
            {
                if (ch == '-') continue;
                if (!Char.IsDigit(ch)) return false;
            }
            return true;
        }
        internal static bool IsValidLongitude(double longitude)
        {
            return longitude >= -180 && longitude <= 180;
        }
        internal static bool IsValidLatitude(double lat)
        {
            return lat >= -90 && lat <= 90;
        }
        internal static bool IsValidName(string name)
        {
            if (name == null) return true;

            foreach (char ch in name)
            {
                if (ch == ' ') continue;
                if (!Char.IsLetter(ch)) return false;
            }
            return true;
        }
        internal static bool IsValidEnumOption<T>(int option)
        {
            return option >= 0 && option < Enum.GetValues(typeof(T)).Length;
        }
    }
}
