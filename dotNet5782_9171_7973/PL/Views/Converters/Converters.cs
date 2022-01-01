using PO;
using System;
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

    public class ItemToExtandedListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ILocalable localable = (ILocalable)value;

            return new List<object>() { 
                new 
                { 
                    localable.Location.Latitude, 
                    localable.Location.Longitude, 
                    Name = value.GetType().Name, 
                    Color = ColorsDictionary[value.GetType()].ToString()
                } 
            };
        }

        public Dictionary<Type, Color> ColorsDictionary { get; set; } = new()
        {
            [typeof(Drone)] = Colors.Pink,
            [typeof(Customer)] = Colors.LightBlue,
            [typeof(Parcel)] = Colors.Lavender,
            [typeof(BaseStation)] = Colors.Lavender,
        };

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

    public class ParcelToSteperIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parcel = PLService.GetParcel((int)value);
            return parcel.Scheduled == null ?
                0 : parcel.PickedUp == null ?
                1 : parcel.Supplied == null ?
                2 : 3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateToToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ?? "Not yet";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
