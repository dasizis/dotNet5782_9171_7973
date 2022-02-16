using PO;
using StringUtilities;
using System.Windows;
using System.Collections.ObjectModel;

namespace PL.ViewModels
{
    class ParcelDetailsViewModel : DependencyObject
    {
        /// <summary>
        /// The parcel model
        /// </summary>
        public Parcel Parcel { get; set; } = new();

        /// <summary>
        /// List of markers for the map
        /// </summary>
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        /// <summary>
        /// A command to view customer who sends the parcel
        /// </summary>
        public RelayCommand ViewSenderCommand { get; set; }

        /// <summary>
        /// A command to view customer who gets the parcel
        /// </summary>
        public RelayCommand ViewTargetCommand { get; set; }

        /// <summary>
        /// A command to view drone which delivers the parcel
        /// </summary>
        public RelayCommand ViewDroneCommand { get; set; }

        /// <summary>
        /// A command to delete the parcel
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Constractor 
        /// Initialize the parcel and commands
        /// </summary>
        /// <param name="id">id of the parcel model</param>
        public ParcelDetailsViewModel(int id)
        {
            Parcel.Id = id;
            LoadParcel();

            PLNotification.ParcelNotification.AddHandler(LoadParcel, id);

            ViewSenderCommand = new(ViewSenderDetails);
            ViewTargetCommand = new(ViewTargetDetails);
            ViewDroneCommand = new(ViewDroneDetails, () => Parcel.Scheduled != null);
            DeleteCommand = new(Delete, () => Parcel.Scheduled == null || Parcel.Supplied != null);     
        }

        /// <summary>
        /// Open details of customer who sends the parcel
        /// </summary>
        private void ViewSenderDetails()
        {
            try
            {
                PLService.GetCustomer(Parcel.Sender.Id);
            }
            catch(BO.ObjectNotFoundException)
            {
                MessageBox.Show(MessageBox.BoxType.Error, $"Sender customer #{Parcel.Sender.Id} was deleted. ");
                return;
            }
            Workspace.AddPanelCommand.Execute(Workspace.CustomerPanel(Parcel.Sender.Id));
        }

        /// <summary>
        /// Open details of customer who gets the parcel
        /// </summary>
        private void ViewTargetDetails()
        {
            try
            {
                PLService.GetCustomer(Parcel.Target.Id);
            }
            catch (BO.ObjectNotFoundException)
            {
                MessageBox.Show(MessageBox.BoxType.Error, $"Target customer #{Parcel.Target.Id} was deleted.");
                return;
            }
            Workspace.AddPanelCommand.Execute(Workspace.CustomerPanel(Parcel.Target.Id));
        }

        /// <summary>
        /// Open details of drone which delivers the parcel
        /// </summary>
        private void ViewDroneDetails()
        {
            try
            {
                PLService.GetDrone((int)Parcel.DroneId);
            }
            catch (BO.ObjectNotFoundException)
            {
                MessageBox.Show(MessageBox.BoxType.Error, $"Deliver Drone #{(int)Parcel.DroneId} was deleted. ");
                return;
            }
            Workspace.AddPanelCommand.Execute(Workspace.DronePanel(Parcel.DroneId));
        }

        /// <summary>
        /// Delete parcel
        /// </summary>
        private void Delete()
        {
            PLService.DeleteParcel(Parcel.Id);
            Workspace.RemovePanelCommand.Execute(Workspace.ParcelPanelName(Parcel.Id));
            MessageBox.Show(MessageBox.BoxType.Info, $"Parcel #{Parcel.Id} deleted");
        }

        /// <summary>
        /// Load parcel
        /// </summary>
        private void LoadParcel()
        {
            Parcel.Reload(PLService.GetParcel(Parcel.Id));

            //load map
            Markers.Clear();
            if (Parcel.DroneId != null)
            {
                Markers.Add(MapMarker.FromType(PLService.GetDrone((int)Parcel.DroneId)));
            }

            Markers.Add(MapMarker.FromTypeAndName(PLService.GetCustomer(Parcel.Sender.Id), "Sender Customer"));
            Markers.Add(MapMarker.FromTypeAndName(PLService.GetCustomer(Parcel.Target.Id), "Target Customer"));
        }
    }
}

