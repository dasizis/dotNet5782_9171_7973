using BO;
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
    /// Interaction logic for droneDisplay.xaml
    /// </summary>
    public partial class DroneDisplay : UserControl
    {
        public BLApi.IBL bal { get; set; }
        bool isDeliver;
        public DroneDisplay(int id)
        {
            bal = BLApi.FactoryBL.GetBL();
            Drone drone = bal.GetDrone(id);
            DataContext = drone;
            isDeliver = drone.State == DroneState.Deliver ? false : true;
            InitializeComponent();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new();
            textBlock.Text = ParcelId.Text;
            t.Children.Add(textBlock);
        }


        
    }
}
