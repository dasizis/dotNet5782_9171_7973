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
    /// Interaction logic for AddDrone.xaml
    /// </summary>
    public partial class AddDrone : UserControl
    {
        public BLApi.IBL bal { get; set; }
        public AddDrone()
        {
            bal = BLApi.FactoryBL.GetBL();
            
            InitializeComponent();

            WeightCategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory)); 
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bal.AddDrone(int.Parse(IdTextBox.Text), 
                         ModelTextBox.Text, 
                         (BO.WeightCategory)WeightCategoryComboBox.SelectedIndex,
                         int.Parse(BaseStationIdTextBox.Text));

        }
    }
}
