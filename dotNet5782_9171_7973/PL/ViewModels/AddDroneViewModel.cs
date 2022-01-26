using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PO;

namespace PL.ViewModels
{
    class AddDroneViewModel : AddItemViewModel<DroneToAdd>
    {
        public DroneToAdd Drone => Model;

        public Array WeightOptions { get; } = Enum.GetValues(typeof(WeightCategory));

        public ObservableCollection<int> StationsOptions { get; set; } = new();

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
            StationsOptions.Clear();

            foreach (var station in PLService.GetAvailableBaseStations())
            {
                StationsOptions.Add(station.Id);
            }
        }
    }
}
