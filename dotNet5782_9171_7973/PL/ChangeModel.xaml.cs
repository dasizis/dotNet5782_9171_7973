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
    /// Interaction logic for ChangeModel.xaml
    /// </summary>
    public partial class ChangeModel : UserControl
    {
        public BLApi.IBL bal { get; set; }


        public int DroneId
        {
            get { return (int)GetValue(DroneIdProperty); }
            set { SetValue(DroneIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DroneId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DroneIdProperty =
            DependencyProperty.Register("DroneId", typeof(int), typeof(ChangeModel), new PropertyMetadata(0));


        public ChangeModel()
        {
            bal = BLApi.FactoryBL.GetBL();
            InitializeComponent();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            bal.RenameDrone((int)DroneId, ModelTextBox.Text);
            DronesHandlers.NotifyDroneChanged(this, DroneId);
        }
    }
}
