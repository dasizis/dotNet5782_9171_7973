using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : UserControl
    {
        public enum BoxType { Success, Error, Info, Warning }

        private MessageBox()
        {
            InitializeComponent();
        }

        public static void Show(BoxType type, string text, double width = 400)
        {
            var dialog = new MessageBox()
            {
                DataContext = new { Type = type, Text = text, Width = width }
            };

            DialogHost.Show(dialog);
        }
    }
}
