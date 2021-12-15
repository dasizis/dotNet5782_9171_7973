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
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : UserControl
    {
        public Type FilterBy { get; set; } = null;
        public int Selcted { get; set; }
        public DronesList()
        {
            InitializeComponent();
            DataContext = BLApi.FactoryBL.GetBL().GetDronesList();
        }
    }    
}
