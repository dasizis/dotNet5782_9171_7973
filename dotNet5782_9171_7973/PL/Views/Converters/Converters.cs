using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Views.Converters
{

    public class ItmeToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new List<object>() { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DroneStateToHandleChargeText : IValueConverter
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

    public class StateToProceedWithParcelText : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] == DependencyProperty.UnsetValue) return "Assign Parcel to Drone";

            PO.DroneState state = (PO.DroneState)values[0];
            bool wasPickedUp = (bool)values[1];

            return state == PO.DroneState.Deliver 
                   ? wasPickedUp ? "Supply Parcel" : "Pick Parcel Up"
                   : "Assign Parcel to Drone";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DroneStateToAnimationSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (PO.DroneState)value == PO.DroneState.Maintenance ? 30 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
