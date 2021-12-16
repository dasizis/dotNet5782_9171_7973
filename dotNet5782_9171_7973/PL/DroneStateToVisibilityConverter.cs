using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    class FreeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (BO.DroneState)value == BO.DroneState.Free? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class MaintenanceToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (BO.DroneState)value == BO.DroneState.Maintenance ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class DeliverToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (BO.DroneState)value == BO.DroneState.Deliver ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class CollectedToVIsibilityConverter : IValueConverter
    {
        public BLApi.IBL bal { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parcel = BLApi.FactoryBL.GetBL().GetParcel((int)value);
            return parcel.PickedUp == null ? Visibility.Visible: Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class SuppliedToVIsibilityConverter : IValueConverter
    {
        public BLApi.IBL bal { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parcel = BLApi.FactoryBL.GetBL().GetParcel((int)value);
            return (parcel.Supplied == null && parcel.PickedUp != null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

