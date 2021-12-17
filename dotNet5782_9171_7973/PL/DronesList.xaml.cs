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
            bal = BLApi.FactoryBL.GetBL();
            Drones = new (bal.GetDronesList());
            DataContext = this;
            InitializeComponent();
            DronesHandlers.DroneChangedEvent += DronesHandlers_DroneChangedEvent;

        }

        private void DronesHandlers_DroneChangedEvent(object sender, DroneChangedEventArg e)
        {
            LoadDrones();
        }

        private void c()
        {
            DataContext = bal.GetDronesList();
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


        private void Drone_Click(object sender, MouseButtonEventArgs e)
        {
            OpenNewTab.AddDroneDisplayTab(((sender as FrameworkElement).Tag as BO.DroneForList).Id, c);
        }
    }    
}
