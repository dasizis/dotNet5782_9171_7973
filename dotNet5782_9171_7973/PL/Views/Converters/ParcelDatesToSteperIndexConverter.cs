using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PL.Views.Converters
{
    public class ParcelDatesToSteperIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!values.Contains(null)) return values.Length - 1;
            return values.ToList().FindIndex(value => value == null) - 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
