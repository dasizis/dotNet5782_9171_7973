using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class StationDetailsViewModel 
    {
        public BaseStation station { get; set; }
        public RelayCommand<object> UpdateDetails { get; set; }
        public RelayCommand<object> OpenDronesList { get; set; }

        public StationDetailsViewModel(int id)
        {
            station = PLService.GetBaseStation(id);

            UpdateDetails = new(ExecuteUpdateDetails);
            OpenDronesList = new(ExecuteOpenDronesList);

            BaseStationsNotification.BaseStationsChangedEvent += LoadStation;
        }

        private void ExecuteUpdateDetails(object param)
        {
            PLService.UpdateBaseStation(station.Id, station.Name, station.EmptyChargeSlots);
        }

        private void ExecuteOpenDronesList(object param)
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
