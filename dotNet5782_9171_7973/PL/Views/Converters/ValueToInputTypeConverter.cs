using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace PL.Views.Converters
{
    public class ValueToInputTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return InputType.Text;

            if (((PropertyInfo)value).PropertyType == typeof(string))
                return InputType.Text;

            if (((PropertyInfo)value).PropertyType == typeof(int) ||
                ((PropertyInfo)value).PropertyType == typeof(int?) ||
                ((PropertyInfo)value).PropertyType == typeof(double) ||
                ((PropertyInfo)value).PropertyType == typeof(double?))
                return InputType.Range;

            if (((PropertyInfo)value).PropertyType.IsEnum)
                return InputType.ComboBox;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

