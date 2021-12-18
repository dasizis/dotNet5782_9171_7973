using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for DroneControl.xaml
    /// </summary>
    public partial class DroneControl : UserControl
    {

        public int? Id { get; set; } 

        public string Model { get; set; }

        public int? SelectedStation { get; set; }

        public BO.WeightCategory? SelectedWeight { get; set; } = null;

        public Array WeightOptions { get; set; } = Enum.GetValues(typeof(BO.WeightCategory));

        public List<int> StationsOptions { get; set; }

        public BO.DroneState State { get; set; }

        public BO.ParcelInDeliver Parcel { get; set; }

        public BLApi.IBL bal { get; set; }

        public DroneControl()
        {
            InitializeComponent();

            DataContext = this;
            bal = BLApi.FactoryBL.GetBL();
            StationsOptions = bal.GetAvailableBaseStations().Select(s => s.Id).ToList();
        }

        private void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            string message = null;
            bool didSucceed = false;

            if (State == BO.DroneState.Free)
            {
                try
                {
                    bal.AssignParcelToDrone((int)Id);
                    didSucceed = true;
                    message = "A parcel assigned to drone";
                }
                catch (BO.InValidActionException)
                {
                    message = "No parcel to assign";
                }
            }
            else
            {
                if (!Parcel.Position)
                {
                    bal.PickUpParcel((int)Id);
                    didSucceed = true;
                    message = "Parcel picked up";
                }
                else
                {
                    bal.SupplyParcel((int)Id);
                    didSucceed = true;
                    message = "Parcel Supplied";
                }
            }

            DialogHost.OpenDialogCommand.Execute(
                didSucceed
                ? new Success() { TextContent = message }
                : new Success() { TextContent = message }
                , null
            );


        }

        private void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            string message = null;
            bool didSucceed = false;

            if (State == BO.DroneState.Free)
            {
                try
                {
                    bal.ChargeDrone((int)Id);
                    didSucceed = true;
                    message = "Drone was send to charge";
                }
                catch (BO.InValidActionException)
                {
                    message = "Can not get to station";
                }
            }
            else if (State == BO.DroneState.Maintenance)
            {
                try
                {
                    bal.FinishCharging((int)Id);
                    didSucceed = true;
                    message = "Drone realesed from charging";
                }
                catch (BO.InValidActionException ex)
                {
                    message = ex.Message;
                }

            }

            DialogHost.OpenDialogCommand.Execute(
                didSucceed
                ? new Success() { TextContent = message }
                : new Success() { TextContent = message }
                , null
            );


        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bal.RenameDrone((int)Id, Model);
            DialogHost.OpenDialogCommand.Execute(new Success() { TextContent = "Drone renamed"},null );
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bal.AddDrone((int)Id, Model, (BO.WeightCategory)SelectedWeight, (int)SelectedStation);
            DialogHost.OpenDialogCommand.Execute(new Success() { TextContent = "New drone added" }, null);
        }
    }
}
