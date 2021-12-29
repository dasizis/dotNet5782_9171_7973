﻿using System.Windows;
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
            BaseStationsNotification.BaseStationsChangedEvent += LoadStation;

            Station = PLService.GetBaseStation(id);

            UpdateDetailsCommand = new(ExecuteUpdateDetails, () => Station.Error == null);
            OpenDronesListCommand = new(ViewDronesList);
            DeleteCommand = new(Delete, () => Station.DronesInCharge.Count == 0);
        }

        private void ExecuteUpdateDetails()
        {
            PLService.UpdateBaseStation(Station.Id, Station.Name, Station.EmptyChargeSlots);
            MessageBox.Show($"Station {Station.Id}' details changed");
        }

        private void ViewDronesList()
        {
            MessageBox.Show("Open drones list");
        }

        public void Delete()
        {
            BaseStationsNotification.BaseStationsChangedEvent -= LoadStation;

            PLService.DeleteBaseStation(Station.Id);
            MessageBox.Show($"Station {Station.Id} deleted");
        }

        private void LoadStation()
        {
            int emptyslots = Station.EmptyChargeSlots;
            string name = Station.Name;

            Station.Reload(PLService.GetBaseStation(Station.Id));

            Station.EmptyChargeSlots = emptyslots;
            Station.Name = name;
        }
    }
}
