using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

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
