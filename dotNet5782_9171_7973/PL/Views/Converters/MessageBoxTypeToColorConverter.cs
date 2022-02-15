using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Views.Converters
{
    class MessageBoxTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MessageBox.BoxType)value switch
            {
                MessageBox.BoxType.Success => Color.Green,
                MessageBox.BoxType.Error => Color.Red,
                MessageBox.BoxType.Warning => Color.Yellow,
                MessageBox.BoxType.Info => Color.LightBlue,
                _ => Color.Black,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
