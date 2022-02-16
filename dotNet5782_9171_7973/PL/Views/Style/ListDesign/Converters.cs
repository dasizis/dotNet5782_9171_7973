using PO;
using StringUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.Views.Style.ListDesign
{
    class ObjectToTreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.GetType()
                        .GetProperties(BindingFlags.Public |
                                       BindingFlags.Instance |
                                       BindingFlags.DeclaredOnly)
                        .Select(prop => CreateProperty(prop, value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static object CreateProperty(PropertyInfo prop, object owner)
        {
            var propValue = prop.GetValue(owner);

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
     class CollapseListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IEnumerable)value).Cast<object>()
                                       .GetType()
                                       .GetProperties(BindingFlags.Public |
                                                      BindingFlags.Instance |
                                                      BindingFlags.DeclaredOnly)
                                       .Select(prop => CreateProperty(prop, value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static object CreateProperty(PropertyInfo prop, object owner)
        {
            var propValue = prop.GetValue(owner);

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

    class TypeAndIdToPanelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return PanelsDictionary[(Type)values[0]]((int)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Type, Func<int?, ViewModels.Panel>> PanelsDictionary { get; set; } = new()
        {
            [typeof(Drone)] = ViewModels.Workspace.DronePanel,
            [typeof(Customer)] = id => ViewModels.Workspace.CustomerPanel(id),
            [typeof(Parcel)] = ViewModels.Workspace.ParcelPanel,
            [typeof(BaseStation)] = ViewModels.Workspace.BaseStationPanel,
        };
    }

    class ObjectToTreeViewProperitesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.GetType()
                        .GetProperties(BindingFlags.Public |
                                       BindingFlags.Instance |
                                       BindingFlags.DeclaredOnly)
                        .Select(prop => CreateProperty(prop, value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static object CreateProperty(PropertyInfo prop, object owner)
        {
            var propValue = prop.GetValue(owner);

            if (prop.PropertyType == typeof(PO.Location)) return propValue;

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
            var location = value as PO.Location;
            return new List<object>() { new Longitude() { Long = (double)location.Longitude }, new Latitude() { Lat = (double)location.Latitude } };
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
