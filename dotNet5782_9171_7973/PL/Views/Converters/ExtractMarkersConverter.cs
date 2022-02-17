using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Views.Converters
{
    class ExtractMarkersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

            //return ((IEnumerable<PL.ViewModels.ItemMarker>)value).Select(itemMarker => itemMarker.Marker);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
