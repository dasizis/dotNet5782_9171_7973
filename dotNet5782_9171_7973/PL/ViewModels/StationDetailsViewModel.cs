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

            UpdateDetails = new RelayCommand<object>(ExecuteUpdateDetails, param => station.Error == null);
            OpenDronesList = new RelayCommand<object>(ExecuteOpenDronesList, param => station.Error == null);

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

        private void LoadStation(object param)
        {
            station = PLService.GetBaseStation(station.Id);
        }
    }
}
