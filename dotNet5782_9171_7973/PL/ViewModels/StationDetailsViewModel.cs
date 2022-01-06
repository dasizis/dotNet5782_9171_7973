using System;
using System.Collections.ObjectModel;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class StationDetailsViewModel 
    {
        public BaseStation Station { get; set; } = new();
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        public RelayCommand UpdateDetailsCommand { get; set; }
        public RelayCommand OpenDronesListCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public StationDetailsViewModel(int id)
        {
            BaseStationsNotification.BaseStationsChangedEvent += LoadStation;
            DronesNotification.DronesChangedEvent += LoadStation;

            Station.Id = id;
            LoadStation();

            UpdateDetailsCommand = new(ExecuteUpdateDetails, () => Station.Error == null);
            DeleteCommand = new(Delete, () => Station.DronesInCharge.Count == 0);
            OpenDronesListCommand = new(() => Views.WorkspaceView.AddPanelCommand.Execute(Workspace.DronesListPanel((d) => Station.DronesInCharge.Exists(drone => drone.Id == d.Id), Workspace.StationChrgedDronesName(Station.Id))),
                                    () => Station.DronesInCharge.Count != 0);
        }

        private void ExecuteUpdateDetails()
        {
            PLService.UpdateBaseStation(Station.Id, Station.Name, Station.EmptyChargeSlots);
            MessageBox.Show($"Station {Station.Id}' details changed");
        }

        public void Delete()
        {
            BaseStationsNotification.BaseStationsChangedEvent -= LoadStation;

            PLService.DeleteBaseStation(Station.Id);
            MessageBox.Show($"Station {Station.Id} deleted");
        }

        private void LoadStation()
        {
            Station.Reload(PLService.GetBaseStation(Station.Id));
            Markers.Clear();
            Markers.Add(MapMarker.FromType(Station));
        }
    }
}
