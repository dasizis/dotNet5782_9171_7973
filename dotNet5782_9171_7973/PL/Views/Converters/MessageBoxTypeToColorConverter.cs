using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace PL.Views.Converters
{
    class MessageBoxTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MessageBox.BoxType)value switch
            {
                MessageBox.BoxType.Success => "#b1d3a7",
                MessageBox.BoxType.Error => "#ff6969",
                MessageBox.BoxType.Warning => "#f4e171",
                MessageBox.BoxType.Info => nameof(Color.LightBlue),
                _ => Color.Black,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
