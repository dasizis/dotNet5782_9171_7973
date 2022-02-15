using System.Collections.ObjectModel;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class StationDetailsViewModel 
    {
        /// <summary>
        /// The base station model
        /// </summary>
        public BaseStation Station { get; set; } = new();

        /// <summary>
        /// List of markers for the map
        /// </summary>
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        /// <summary>
        /// A command to update base station details
        /// </summary>
        public RelayCommand UpdateDetailsCommand { get; set; }

        /// <summary>
        /// A command to open list of drones which are being charges at station
        /// </summary>
        public RelayCommand OpenDronesListCommand { get; set; }

        /// <summary>
        /// A command to delete base station
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Constractor
        /// Initialize base station and commands
        /// </summary>
        /// <param name="id">id of base station model</param>
        public StationDetailsViewModel(int id)
        {
            Station.Id = id;
            LoadStation();

            PLNotification.BaseStationNotification.AddHandler(LoadStation, id);

            UpdateDetailsCommand = new(ExecuteUpdateDetails, () => Station.Error == null);
            DeleteCommand = new(Delete, () => Station.DronesInCharge.Count == 0);
            OpenDronesListCommand = new(
                () => Workspace.AddPanelCommand.Execute(Workspace.FilteredDronesListPanel(d => Station.DronesInCharge.Exists(drone => drone.Id == d.Id), 
                                                                                  Workspace.StationChrgedDronesName(Station.Id))),
                () => Station.DronesInCharge.Count != 0);
        }

        /// <summary>
        /// Update details of base station
        /// </summary>
        private void ExecuteUpdateDetails()
        {
            PLService.UpdateBaseStation(Station.Id, Station.Name, Station.EmptyChargeSlots);
            MessageBox.Show(MessageBox.BoxType.Success, $"Station {Station.Id}' details changed");
        }

        /// <summary>
        /// Delete base station
        /// </summary>
        public void Delete()
        {
            PLService.DeleteBaseStation(Station.Id);
            MessageBox.Show(MessageBox.BoxType.Info, $"Station {Station.Id} deleted");
        }

        /// <summary>
        /// Load base station
        /// </summary>
        private void LoadStation()
        {
            Station.Reload(PLService.GetBaseStation(Station.Id));
            //load map
            Markers.Clear();
            Markers.Add(MapMarker.FromType(Station));
        }
    }
}
