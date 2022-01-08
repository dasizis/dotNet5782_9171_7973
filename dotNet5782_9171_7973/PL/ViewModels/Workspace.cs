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

    //I changed here to make it work from ContentControl to UserControl
    public record Panel(PanelType PanelType, ContentControl View, string Header);

    static class Workspace
    {
        public static string EntityPanelName(string name, int? id = null) => id == null ? $"Add {name}" : $"{name} #{id}";

        public static string DronePanelName(int? id = null) => EntityPanelName(nameof(PO.Drone), id);
        public static string ParcelPanelName(int? id = null) => EntityPanelName(nameof(PO.Parcel), id);
        public static string CustomerPanelName(int? id = null) => EntityPanelName(nameof(PO.Customer), id);
        public static string BaseStationPanelName(int? id = null) => EntityPanelName(nameof(PO.BaseStation), id);
        //public static string AddDronePanelName() => $"{nameof(Views.AddDroneView).CamelCaseToReadable()}";
        //public static string AddParcelPanelName() => $"{nameof(Views.AddParcelView).CamelCaseToReadable()}";
        //public static string AddCustomerPanelName() => $"{nameof(Views.AddCustomerView).CamelCaseToReadable()}";
        //public static string AddBaseStationPanelName() => $"{nameof(Views.AddStationView).CamelCaseToReadable()}";

        public static string CustomerSentListName(int id) => $"Customer {id} Sent Parcels";
        public static string CustomerRecievedListName(int id) => $"Customer {id} Recieved Parcels";
        public static string StationChrgedDronesName(int id) => $"Station #{id} Charged Drones";

        public static string ListPanelName(Type type) => $"{type.Name} List";

        public static Panel DronePanel(int? id = null) => id == null 
            ? new(PanelType.Add, new Views.AddDroneView(), DronePanelName()) 
            : new(PanelType.Display, new Views.DroneDetailsView((int)id), DronePanelName(id));

        public static Panel ParcelPanel(int? id = null) => id == null 
            ? new(PanelType.Add, new Views.AddParcelView(), ParcelPanelName())
            : new(PanelType.Display, new Views.ParcelDetailsView((int)id), ParcelPanelName(id));

        public static Panel CustomerPanel(int? id = null) => id == null 
            ? new(PanelType.Add, new Views.AddCustomerView(), CustomerPanelName())
            : new(PanelType.Display, new Views.CustomerDetailsView((int)id), CustomerPanelName(id));

        public static Panel BaseStationPanel(int? id = null) => id == null
            ? new(PanelType.Add, new Views.AddStationView(), BaseStationPanelName())
            : new(PanelType.Display, new Views.StationDetailsView((int)id), BaseStationPanelName(id));

        public static Panel DronesListPanel(Predicate<PO.DroneForList> predicate, string header) =>
            new(PanelType.List, new Views.FilteredDronesListView(predicate), header);

        public static Panel ParcelsListPanel(Predicate<PO.ParcelForList> predicate, string header) =>
            new(PanelType.List, new Views.FilteredParcelsListView(predicate), header);

        public static RelayCommand<Panel> AddPanelCommand => Views.WorkspaceView.AddPanelCommand;
        public static RelayCommand<string> RemovePanelCommand => Views.WorkspaceView.RemovePanelCommand;

        public static readonly string TargerNameOfListPanelType = "ListArea";
    }
}
