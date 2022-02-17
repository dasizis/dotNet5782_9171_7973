using StringUtilities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Views.Converters
{
    class CamelCaseToReadableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).CamelCaseToReadable();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
