using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


    public partial class DroneControl : UserControl, INotifyPropertyChanged
    {
        private double battery;
        private BO.DroneState state;
        private int? selectedStation;
        private BO.ParcelInDeliver parcel;

        public bool IsAddMode { get; set; }
        public bool IsActionsMode { get; set; }

        public int? Id { get; set; }

        public string Model { get; set; }

        public int? SelectedStation { get => selectedStation; set { selectedStation = value; OnPropertyChangedEvent("SelectedStation"); } }

        public BO.WeightCategory? SelectedWeight { get; set; } = null;

        public Array WeightOptions { get; set; } = Enum.GetValues(typeof(BO.WeightCategory));

        public List<int> StationsOptions { get; set; }

        public BO.DroneState State { get => state; set { state = value; OnPropertyChangedEvent("State"); } }

        public BO.ParcelInDeliver Parcel { get => parcel; set { parcel = value; OnPropertyChangedEvent("Parcel"); } }

        public double Battery { get => battery; set { battery = value; OnPropertyChangedEvent("Battery"); } }

        public BO.Location Location { get; set; }

        public BO.WeightCategory MaxWeight { get; set; }

        public BLApi.IBL bal { get; set; }

        public bool IsInCharge { get; set; }


        private void Init()
        {
            InitializeComponent();

            bal = BLApi.FactoryBL.GetBL();
            DataContext = this;
            StationsOptions = bal.GetAvailableBaseStations().Select(s => s.Id).ToList();
        }
        public DroneControl()
        {
            Init();

            IsAddMode = true;
            IsActionsMode = !IsAddMode;

        }
        public DroneControl(int id)
        {
            Init();

            IsAddMode = false;
            IsActionsMode = !IsAddMode;

            LoadDrone(id);
            DronesHandlers.DronesChangedEvent += () => LoadDrone(id);
        }
        private void LoadDrone(int id)
        {
            var drone = bal.GetDrone(id);

            Id = drone.Id;
            Model = drone.Model;
            State = drone.State;
            SelectedWeight = drone.MaxWeight;
            Parcel = drone.ParcelInDeliver;
            Battery = drone.Battery;
            Location = drone.Location;
            MaxWeight = drone.MaxWeight;
            try
            {
                SelectedStation = bal.GetDroneBaseStation(drone.Id);
                IsInCharge = true;
            }
            catch (BO.ObjectNotFoundException)
            {
                SelectedStation = null;
                IsInCharge = false;
            }
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
                : new Failure() { TextContent = message }
                , null
            );

            DronesHandlers.NotifyDroneChanged();

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
                : new Failure() { TextContent = message }
                , null
            );

            DronesHandlers.NotifyDroneChanged();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bal.RenameDrone((int)Id, Model);
            DialogHost.OpenDialogCommand.Execute(new Success() { TextContent = "Drone renamed" }, null);
            DronesHandlers.NotifyDroneChanged();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bal.AddDrone((int)Id, Model, (BO.WeightCategory)SelectedWeight, (int)SelectedStation);
                DialogHost.OpenDialogCommand.Execute(new Success() { TextContent = "New drone added" }, null);
                DronesHandlers.NotifyDroneChanged();
                ((MainWindow)Window.GetWindow(this)).CloseMyTab();
            }
            catch (BO.IdAlreadyExistsException)
            {
                DialogHost.OpenDialogCommand.Execute(new Failure() { TextContent = "Drone id already exists" }, null);

            }

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).CloseMyTab();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
