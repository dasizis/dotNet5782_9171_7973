using StringUtilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace PL.ListDesign
{
    class ObjectToTreeViewProperitesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.GetType().GetProperties().Select(prop => CreateProperty(prop, value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private object CreateProperty(PropertyInfo prop, object owner)
        {
            var propValue = prop.GetValue(owner);

            if (propValue?.GetType() == typeof(BO.Location)) return propValue;

            if (prop.Name == "Battery")
            {
                return new Battery() { BatteryValue = (double)propValue };
            }

            return new TreeViewProp() 
            { 
                PropName = prop.Name.CamelCaseToReadable(), 
                propValue = propValue 
            };
        }
    }

    class LocationToOrientaionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var location = value as BO.Location;
            return new List<object>() { new Longitude() { Long = location.Longitude }, new Latitude() { Lat = location.Latitude } };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class BatteryValueToIconNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int battery = (int)(double)value;

            return $"Battery{battery / 10 * 10}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
