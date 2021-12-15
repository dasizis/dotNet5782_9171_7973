using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<Type> FilterBy { get; set; } = new()
        {
            typeof(BO.WeightCategory),
            typeof(BO.DroneState),
        };

        public int SelectedType { get; set; }
        public int SelectedOption { get; set; }
        public BLApi.IBL bal { get; set; }

        public ObservableCollection<BO.DroneForList> Drones { get; set; }
        public DronesList()
        {
            bal = BLApi.FactoryBL.GetBL();

            DataContext = this;
            Drones = new (bal.GetDronesList()); 
            InitializeComponent();
        }

        private void FilterByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            SelectedType = combo.SelectedIndex;
            ChooseOptionComboBox.ItemsSource = Enum.GetValues(combo.SelectedItem as Type);
        }

        private void ChooseOptionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedOption = (sender as ComboBox).SelectedIndex;
            SelectionChanged();
        }

        private void SelectionChanged()
        {
            Drones.Clear();
            foreach (var item in bal.GetFilteredDronesList(FilterBy[SelectedType], SelectedOption))
            {
                Drones.Add(item);
            }
        }
    }    
}
