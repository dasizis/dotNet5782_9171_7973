using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace PL.Views.Converters
{
    public class ValueToInputEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
