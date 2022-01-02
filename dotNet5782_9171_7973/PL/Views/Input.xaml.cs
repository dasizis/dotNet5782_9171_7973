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

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(Input), new PropertyMetadata(null));


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


        public RelayCommand<object> NotifyValueChangedCommand { get; set; }

        public Input()
        {
            InitializeComponent();
            DataContext = this;

            NotifyValueChangedCommand = new(NotifyValueChanged);
        }


        private void NotifyValueChanged(object value)
        {
            if (value is SfRangeSlider slider)
            {
                Command.Execute(new double[] { slider.RangeStart, slider.RangeEnd });
            }
            else
            {
                Command.Execute(value);
            }
        }

        private void SfRangeSlider_RangeChanged(object sender, Syncfusion.Windows.Controls.Input.RangeChangedEventArgs e)
        {
            var slider = (SfRangeSlider)sender;
            Command.Execute(new double[] { slider.RangeStart, slider.RangeEnd });
        }
    }
}
