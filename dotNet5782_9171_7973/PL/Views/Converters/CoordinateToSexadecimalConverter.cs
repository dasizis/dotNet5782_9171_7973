using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Views.Converters
{
    public class CoordinateToSexadecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (parameter as string) switch
            {
                "Longitude" => StringUtilities.Sexadecimal.Longitude((double)value),
                "Latitude" => StringUtilities.Sexadecimal.Latitude((double)value),
                _ => "Unknown",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
