using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.Views.Converters
{
    class DroneStateToHandleChargeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((PO.DroneState)value) == PO.DroneState.Maintenance ? "Free from Charge" : "Send to Charge";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
