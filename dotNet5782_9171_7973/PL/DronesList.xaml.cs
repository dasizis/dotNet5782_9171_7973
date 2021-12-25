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
        public Array StateOptions { get; set; } = Enum.GetValues(typeof(BO.DroneState)).Cast<BO.DroneState>().Select(o => $"{o}").Append("All").ToArray();
        public Array WeightOptions { get; set; } = Enum.GetValues(typeof(BO.WeightCategory)).Cast<BO.WeightCategory>().Select(o => $"{o}").Append("All").ToArray();

        public BO.DroneState SelectedState
        {
            get { return (BO.DroneState)GetValue(SelectedStateProperty); }
            set { SetValue(SelectedStateProperty, value); LoadDrones(); }
        }


        public static readonly DependencyProperty SelectedStateProperty =
            DependencyProperty.Register("SelectedDroneState", typeof(BO.DroneState), typeof(ComboBox), new PropertyMetadata((BO.DroneState)3));



        public BO.WeightCategory SelectedWeight
        {
            get { return (BO.WeightCategory)GetValue(SelectedWeightProperty); }
            set { SetValue(SelectedWeightProperty, value); LoadDrones(); }
        }

        public static readonly DependencyProperty SelectedWeightProperty =
            DependencyProperty.Register("SelectedWeight", typeof(BO.WeightCategory), typeof(ComboBox), new PropertyMetadata((BO.WeightCategory)3));


        public BLApi.IBL bal { get; set; }

        public ObservableCollection<BO.DroneForList> Drones { get; set; }
        public DronesList()
        {
            bal = BLApi.BLFactory.GetBL();
            Drones = new (bal.GetDronesList());
            DataContext = this;
            InitializeComponent();

        }

        private void LoadDrones()
        {
            Drones.Clear();
            var drones = bal.GetFilteredDronesList(Enum.IsDefined(typeof(BO.DroneState), SelectedState)? (int?)SelectedState: null ,
                                                   Enum.IsDefined(typeof(BO.WeightCategory), SelectedWeight) ? (int?)SelectedWeight : null);
            foreach (var item in drones)
            {
                Drones.Add(item);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).AddDisplayTab("Add Drone", new DroneControl());
        }

        private void DronesListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var drone = (sender as TreeView).SelectedItem as BO.DroneForList;
            if (drone == null) return;

            int id = drone.Id;
            ((MainWindow)Window.GetWindow(this)).AddDisplayTab($"Drone {id}", new DroneControl(id));
        }
    }    
}
