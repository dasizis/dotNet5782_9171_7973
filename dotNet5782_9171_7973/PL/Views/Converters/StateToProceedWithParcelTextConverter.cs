using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PL.Views.Converters
{
    public class StateToProceedWithParcelTextConverter : IMultiValueConverter
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
}
