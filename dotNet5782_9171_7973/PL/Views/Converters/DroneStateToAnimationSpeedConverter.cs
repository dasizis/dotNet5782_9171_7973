using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Views.Converters
{
    public class DroneStateToAnimationSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (PO.DroneState)value == PO.DroneState.Maintenance ? 60 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
