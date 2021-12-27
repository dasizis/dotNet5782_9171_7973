using System.Windows;
using PO;

namespace PL.ViewModels
{
    class StationDetailsViewModel 
    {
        public BaseStation Station { get; set; }
        public RelayCommand UpdateDetailsCommand { get; set; }
        public RelayCommand OpenDronesListCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public StationDetailsViewModel(int id)
        {
            Station = PLService.GetBaseStation(id);

            UpdateDetailsCommand = new(ExecuteUpdateDetails, () => Station.Error == null);
            OpenDronesListCommand = new(ExecuteOpenDronesList);
            DeleteCommand = new(ExecuteDelete, () => Station.DronesInCharge.Count == 0);

            BaseStationsNotification.BaseStationsChangedEvent += LoadStation;
        }

        private void ExecuteUpdateDetails()
        {
            PLService.UpdateBaseStation(Station.Id, Station.Name, Station.EmptyChargeSlots);
            MessageBox.Show($"Station {Station.Id}' details changed");
        }

        private void ExecuteOpenDronesList()
        {
            MessageBox.Show("Open drones list");
        }

        public void ExecuteDelete()
        {
            PLService.DeleteBaseStation(Station.Id);
            MessageBox.Show($"Station {Station.Id} deleted");
        }

        private void LoadStation()
        {
            int emptyslots = Station.EmptyChargeSlots;
            string name = Station.Name;

            Station = PLService.GetBaseStation(Station.Id);

            Station.EmptyChargeSlots = emptyslots;
            Station.Name = name;
        }
    }
}
