using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL
{
    class StateToChargeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (BO.DroneState)value != BO.DroneState.Deliver? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class StateToProceedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (BO.DroneState)value != BO.DroneState.Maintenance ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class DroneToProceedContentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((BO.DroneState)values[0] == BO.DroneState.Maintenance) return null;

            if ((BO.DroneState)values[0] == BO.DroneState.Free)
            {
                return "ASSIGN PARCEL TO DRONE";
            }
            else
            {
                if (values[1] == DependencyProperty.UnsetValue || !(bool)values[1])
                {
                    return "PICK PARCEL UP";
                }
                else
                {
                    return "SUPPLY PARCEL";
                }
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class StateToChargeContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return (BO.DroneState)value == BO.DroneState.Free? "SEND TO CHARGE" : "RELEASE FROM CHARGING";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class AggregateBooleansConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue) return true;
            return values.Aggregate((a, b) => (bool)a && (bool)b);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



