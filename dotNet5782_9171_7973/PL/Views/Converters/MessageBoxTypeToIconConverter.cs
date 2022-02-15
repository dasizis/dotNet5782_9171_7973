using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace PL.Views.Converters
{
    class MessageBoxTypeToIConConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MessageBox.BoxType)value switch
            {
                MessageBox.BoxType.Success => PackIconKind.CheckboxMarkedCircle,
                MessageBox.BoxType.Error => PackIconKind.AlertOctagon,
                MessageBox.BoxType.Warning => PackIconKind.Alert,
                MessageBox.BoxType.Info => PackIconKind.MessageAlert,
                _ => PackIconKind.BellAlert,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
