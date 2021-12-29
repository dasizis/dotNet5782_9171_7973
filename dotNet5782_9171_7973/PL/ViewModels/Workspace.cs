using StringUtilities;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.ViewModels
{
    public enum PanelType
    {
        Display,
        List,
        Other,
    }

    public record Panel(PanelType PanelType, ContentControl View, string Header);

    static class Workspace
    {

        public static string DronePanelName(int id) => $"{nameof(Views.DroneDetailsView).CamelCaseToReadable()} {id}";
        public static string ParcelPanelName(int id) => $"{nameof(Views.ParcelDetailsView).CamelCaseToReadable()} {id}";
        public static string CustomerPanelName(int id) => $"{nameof(Views.CustomerDetailsView).CamelCaseToReadable()} {id}";
        public static string BaseStationPanelName(int id) => $"{nameof(Views.StationDetailsView).CamelCaseToReadable()} {id}";

        public static Panel DronePanel(int? id) => id == null 
            ? null 
            : new(PanelType.Display, new Views.DroneDetailsView((int)id), DronePanelName((int)id));

        public static Panel ParcelPanel(int? id) => id == null 
            ? null
            : new(PanelType.Display, new Views.ParcelDetailsView((int)id), ParcelPanelName((int)id));

        public static Panel CustomerPanel(int? id) => id == null 
            ? null
            : new(PanelType.Display, new Views.CustomerDetailsView((int)id), CustomerPanelName((int)id));

        public static Panel BaseStationPanel(int? id) => id == null
            ? null
            : new(PanelType.Display, new Views.StationDetailsView((int)id), BaseStationPanelName((int)id));

    }
}
