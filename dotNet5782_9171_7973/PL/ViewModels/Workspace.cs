using StringUtilities;
using System;
using System.Windows.Controls;

namespace PL.ViewModels
{
    public enum PanelType
    {
        Display,
        List,
        Add,
        Other,
    }

    public record Panel(PanelType PanelType, ContentControl View, string Header);

    static class Workspace
    {

        public static string DronePanelName(int id) => $"{nameof(Views.DroneDetailsView).CamelCaseToReadable()} {id}";
        public static string ParcelPanelName(int id) => $"{nameof(Views.ParcelDetailsView).CamelCaseToReadable()} {id}";
        public static string CustomerPanelName(int id) => $"{nameof(Views.CustomerDetailsView).CamelCaseToReadable()} {id}";
        public static string BaseStationPanelName(int id) => $"{nameof(Views.StationDetailsView).CamelCaseToReadable()} {id}";
        public static string AddDronePanelName() => $"{nameof(Views.AddDroneView).CamelCaseToReadable()}";
        public static string AddParcelPanelName() => $"{nameof(Views.AddParcelView).CamelCaseToReadable()}";
        public static string AddCustomerPanelName() => $"{nameof(Views.AddCustomerView).CamelCaseToReadable()}";
        public static string AddBaseStationPanelName() => $"{nameof(Views.AddStationView).CamelCaseToReadable()}";
        public static string ListPanelName(Type type) => $"{type.Name}";

        public static Panel DronePanel(int? id = null) => id == null 
            ? new(PanelType.Add, new Views.AddDroneView(), AddDronePanelName()) 
            : new(PanelType.Display, new Views.DroneDetailsView((int)id), DronePanelName((int)id));

        public static Panel ParcelPanel(int? id = null) => id == null 
            ? new(PanelType.Add, new Views.AddParcelView(), AddParcelPanelName())
            : new(PanelType.Display, new Views.ParcelDetailsView((int)id), ParcelPanelName((int)id));

        public static Panel CustomerPanel(int? id = null) => id == null 
            ? new(PanelType.Add, new Views.AddCustomerView(), AddCustomerPanelName())
            : new(PanelType.Display, new Views.CustomerDetailsView((int)id), CustomerPanelName((int)id));

        public static Panel BaseStationPanel(int? id = null) => id == null
            ? new(PanelType.Add, new Views.AddStationView(), AddBaseStationPanelName())
            : new(PanelType.Display, new Views.StationDetailsView((int)id), BaseStationPanelName((int)id));

        public static Panel DronesListPanel(Predicate<PO.DroneForList> predicate) =>
            new(PanelType.List, new Views.DronesListView(predicate), ListPanelName(typeof(PO.Drone)));

        public static Panel ParcelsListPanel(Predicate<PO.ParcelForList> predicate, string header = null) =>
            new(PanelType.List, new Views.ParcelsListView(predicate), header ?? ListPanelName(typeof(PO.Parcel)));

        public static RelayCommand<Panel> AddPanelCommand { get; set; } = Views.WorkspaceView.AddPanelCommand;
        public static RelayCommand<string> RemovePanelCommand { get; set; } = Views.WorkspaceView.RemovePanelCommand;

    }
}
