using PO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PL.Views.Converters
{
    public class InputTypeToVisiblity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (InputType)value == (InputType)parameter ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

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
