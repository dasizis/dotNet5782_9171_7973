using PO;
using StringUtilities;
using System.Windows;
using System.Collections.ObjectModel;

namespace PL.ViewModels
{
    class ParcelDetailsViewModel : DependencyObject
    {
        public Parcel Parcel { get; set; } = new();
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        public RelayCommand ViewSenderCommand { get; set; }
        public RelayCommand ViewTargetCommand { get; set; }
        public RelayCommand ViewDroneCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

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

        private void ViewSenderDetails()
        {
            Workspace.AddPanelCommand.Execute(Workspace.CustomerPanel(Parcel.Sender.Id));
        }

        private void ViewTargetDetails()
        {
            Workspace.AddPanelCommand.Execute(Workspace.CustomerPanel(Parcel.Target.Id));
        }

        private void ViewDroneDetails()
        {
            Workspace.AddPanelCommand.Execute(Workspace.DronePanel(Parcel.DroneId));
        }

        private void Delete()
        {
            PLService.DeleteParcel(Parcel.Id);
            Workspace.RemovePanelCommand.Execute(Workspace.ParcelPanelName(Parcel.Id));
            MessageBox.Show(MessageBox.BoxType.Info, $"Parcel {Parcel.Id} deleted");
        }

        private void LoadParcel()
        {
            Parcel.Reload(PLService.GetParcel(Parcel.Id));

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

