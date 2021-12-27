using System.Windows;
using PO;

namespace PL.ViewModels
{
    class StationDetailsViewModel 
    {
        public BaseStation station { get; set; }
        public RelayCommand UpdateDetailsCommand { get; set; }
        public RelayCommand OpenDronesListCommand { get; set; }

        public StationDetailsViewModel(int id)
        {
            station = PLService.GetBaseStation(id);

            UpdateDetailsCommand = new(ExecuteUpdateDetails, () => station.Error == null);
            OpenDronesListCommand = new(ExecuteOpenDronesList);

            BaseStationsNotification.BaseStationsChangedEvent += LoadStation;
        }

        private void ExecuteUpdateDetails()
        {
            PLService.UpdateBaseStation(station.Id, station.Name, station.EmptyChargeSlots);
        }

        private void ExecuteOpenDronesList()
        {
            MessageBox.Show("Open drones list");
        }

        private void LoadStation()
        {
            int emptyslots = station.EmptyChargeSlots;
            string name = station.Name;

            station = PLService.GetBaseStation(station.Id);

            station.EmptyChargeSlots = emptyslots;
            station.Name = name;
        }
    }
}
