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



        public List<object> ItemsSource
        {
            get { return (List<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(List<object>), typeof(Input), new PropertyMetadata(null));



        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Input), new PropertyMetadata(0.0));


        public double Maximum
        {
            get { return (double)GetValue(MaximunProperty); }
            set { SetValue(MaximunProperty, value); }
        }

        public static readonly DependencyProperty MaximunProperty =
            DependencyProperty.Register("Maximun", typeof(double), typeof(Input), new PropertyMetadata(100.0));


        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register("StartValue", typeof(double), typeof(Input), new PropertyMetadata(0.0));


        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register("EndValue", typeof(double), typeof(Input), new PropertyMetadata(100.0));




        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(Input), new PropertyMetadata(null));

        public Input()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SfRangeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void SfRangeSlider_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void SfRangeSlider_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            Value = new double[] { StartValue, EndValue };
        }

    }
}
