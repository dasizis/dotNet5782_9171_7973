using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class AddDroneViewModel : AddItemViewModel<DroneToAdd>
    {
        public DroneToAdd Drone => Model;

        public Array WeightOptions { get; } = Enum.GetValues(typeof(WeightCategory));

        public List<int> StationsOptions { get; set; }

        public AddDroneViewModel()
        {
            StationsOptions = PLService.GetAvailableBaseStations().Select(station => station.Id).ToList();
        }

        protected override void Add()
        {
            PLService.AddDrone(Drone);
        }
    }
}
