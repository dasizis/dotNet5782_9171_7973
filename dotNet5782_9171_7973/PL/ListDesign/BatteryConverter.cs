using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    class BatteryValueToIconNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int battery = (int)(double)value;

            if (battery == 100) return "Battery";

            // Take the tens digit (the 2th from the end)
            int tens = battery / 10 % 10;
            string tensString = tens != 0 ? tens.ToString() : string.Empty;

            return $"Battery{tensString}0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
