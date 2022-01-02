using PO;
using StringUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL.Views;
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
            ParcelsNotification.ParcelsChangedEvent += LoadParcel;

            Parcel.Id = id;
            LoadParcel();

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
            Views.WorkspaceView.AddPanelCommand.Execute(
                new Panel(PanelType.Display,
                          new Views.CustomerDetailsView(Parcel.Sender.Id),
                          $"{nameof(Views.CustomerDetailsView).CamelCaseToReadable()} {Parcel.Sender.Id}"));
        }

        private void ViewDroneDetails()
        {
            Views.WorkspaceView.AddPanelCommand.Execute(
                            new Panel(PanelType.Display,
                                      new Views.DroneDetailsView((int)Parcel.DroneId),
                                      $"{nameof(Views.CustomerDetailsView).CamelCaseToReadable()} {Parcel.DroneId}"));
        }

        private void Delete()
        {
            ParcelsNotification.ParcelsChangedEvent -= LoadParcel;

            PLService.DeleteParcel(Parcel.Id);
            Workspace.RemovePanelCommand.Execute(Workspace.ParcelPanelName(Parcel.Id));
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
            Markers.Add(MapMarker.FromTypeAndName(PLService.GetCustomer(Parcel.Sender.Id), "Target Customer"));
        }
    }
}
