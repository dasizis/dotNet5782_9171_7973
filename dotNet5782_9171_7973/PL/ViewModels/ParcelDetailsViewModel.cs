using PO;
using StringUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL.Views;

namespace PL.ViewModels
{
    class ParcelDetailsViewModel : DependencyObject
    {
        public Parcel Parcel { get; set; }
        public RelayCommand ViewSenderCommand { get; set; }
        public RelayCommand ViewTargetCommand { get; set; }
        public RelayCommand ViewDroneCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public ParcelDetailsViewModel(int id)
        {
            ParcelsNotification.ParcelsChangedEvent += LoadParcel;

            Parcel = PLService.GetParcel(id);

            ViewSenderCommand = new(ViewSenderDetails);
            ViewTargetCommand = new(ViewTargetDetails);
            ViewDroneCommand = new(ViewDroneDetails, () => Parcel.Scheduled != null);
            DeleteCommand = new(Delete, () => Parcel.Scheduled == null || Parcel.Supplied != null);     
        }

        private void ViewSenderDetails()
        {
            Views.WorkspaceView.AddPanelCommand.Execute(
                new Panel(PanelType.Display, 
                          new Views.CustomerDetailsView(Parcel.Target.Id), 
                          $"{nameof(Views.CustomerDetailsView).CamelCaseToReadable()} {Parcel.Target.Id}"));
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
            WorkspaceView.RemovePanelCommand.Execute($"{nameof(Views.ParcelDetailsView).CamelCaseToReadable()} {Parcel.Id}");
        }

        private void LoadParcel()
        {
            Parcel.Reload(PLService.GetParcel(Parcel.Id));
        }
    }
}
