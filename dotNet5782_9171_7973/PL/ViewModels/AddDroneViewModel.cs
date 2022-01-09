using System;
using System.Collections.Generic;
using System.Linq;
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
            Load();
            PLNotification.AddHandler<BaseStation>(Load);
        }

        protected override void Add()
        {
            PLService.AddDrone(Drone);
            Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName());
        }

        void Load()
        {
            StationsOptions = PLService.GetAvailableBaseStations().Select(station => station.Id).ToList();
        }
    }
}
