using Syncfusion.DocIO.DLS;
using Syncfusion.Windows.Controls.Input;
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

namespace PL.Views
{
    public enum InputType
    {
        Text,
        Int,
        Double,
        ComboBox,
        Range,
    }
    /// <summary>
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class Input : UserControl
    {
        public InputType Type
        {
            get { return (InputType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(InputType), typeof(Input), new PropertyMetadata(InputType.Text));


        public Type ComboBoxItemsSourceEnumType
        {
            get { return (Type)GetValue(ComboBoxItemsSourceEnumTypeProperty); }
            set { SetValue(ComboBoxItemsSourceEnumTypeProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxItemsSourceEnumTypeProperty =
            DependencyProperty.Register("ComboBoxItemsSourceEnumType", typeof(Type), typeof(Input), new PropertyMetadata(null));

        public RelayCommand<object> Command
        {
            get { return (RelayCommand<object>)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(RelayCommand<object>), typeof(Input), new PropertyMetadata(null));

        public Input()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Command.Execute(((ComboBox)sender).SelectedItem);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Command.Execute(((TextBox)sender).Text);
        }

        private void RangeInput_RangeChanged(object sender, RangeChangedEventArgs e)
        {
            Command.Execute(new double[] { (double)e.NewStartValue, (double)e.NewEndValue });
        }
    }
}
