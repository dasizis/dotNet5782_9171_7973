using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    class ObjectToTreeViewPropsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value.GetType()
                        .GetProperties()
                        .Select(prop =>
                        {
                            var propValue = prop.GetValue(value);
                            if (propValue?.GetType() == typeof(BO.Location))
                            {
                                return new TreeViewLocationProp() { Location = propValue as BO.Location } as object;
                            }

                            return new TreeViewProp() { PropName = prop.Name, propValue = propValue } as object;
                        });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class LocationToOrientaionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var location = (value as TreeViewLocationProp).Location;
            return new List<Object>() { new Longitude() { Long = location.Longitude }, new Latitude() { Lat = location.Latitude } };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
