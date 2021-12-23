using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PO;

namespace PL
{
    class AddDroneViewModel
    {
        public DroneToAdd Drone { get; set; } = new();

        public Array WeightOptions { get; set; } = Enum.GetValues(typeof(BO.WeightCategory));

        public List<int> StationsOptions { get; set; }

        public RelayCommand<object> AddDroneCommand { get; set; }

        public AddDroneViewModel()
        {
            AddDroneCommand = new RelayCommand<object>(AddDrone, param => Drone.Error == null);
            StationsOptions = BLApi.BLFactory.GetBL().GetAvailableBaseStations().Select(station => station.Id).ToList();
        }

        private void AddDrone(object parameter)
        {
            MessageBox.Show("Add drone");
        }
    }
}
