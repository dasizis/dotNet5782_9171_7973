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
        public DroneDisplay()
        {
            bal = BLApi.FactoryBL.GetBL();

            InitializeComponent();

            DataContext = bal.GetDrone(1);
        }
    }
}
