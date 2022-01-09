using PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PL.Views.Converters
{
    static class Mapping
    {
        public static object ConvertToMarker(Location location, Type type, string header = null)
        {
            return new
            {
                location.Latitude,
                location.Longitude,
                Name = header ?? type.Name,
                Color = ColorsDictionary[type].ToString()
            };
        }

        public static Dictionary<Type, Color> ColorsDictionary { get; set; } = new()
        {
            [typeof(Drone)] = Colors.Pink,
            [typeof(Customer)] = Colors.LightBlue,
            [typeof(Parcel)] = Colors.Lavender,
            [typeof(BaseStation)] = Colors.Lavender,
        };
    }

    class PairsOfLocationTypeToMarkers : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(DependencyProperty.UnsetValue))
                return null;

            List<object> markers = new();

            for (int i = 0; i < values.Length; i += 2)
            {
                markers.Add(Mapping.ConvertToMarker((Location)values[i], (Type)values[i + 1]));
            }

            return markers;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    class AggregateListsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.ToList().Cast<IEnumerable<object>>().Aggregate((list1, list2) => list1.Concat(list2));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class TripletsOfLocationTypeHeaderToMarkers : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<object> markers = new();

            for (int i = 0; i < values.Length; i += 3)
            {
                markers.Add(Mapping.ConvertToMarker((Location)values[i], (Type)values[i + 1], (string)values[i + 2]));
            }

            return markers;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ListsToMarkers : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(DependencyProperty.UnsetValue))
                return null;

            List<object> markers = new();

            foreach (var item in values)
            {
                markers.AddRange(((IList)item)
                       .Cast<ILocalable>()
                       .Select(locable => Mapping.ConvertToMarker(locable.Location, locable.GetType())));
            }

            return markers;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
