using System;
using System.Collections.ObjectModel;
using PO;

namespace PL.ViewModels
{
    /// <summary>
    /// A class to represent a view model of Add drone view
    /// </summary>
    class AddDroneViewModel : AddItemViewModel<DroneToAdd>
    {
        /// <summary>
        /// the drone view model's model
        /// </summary>
        public DroneToAdd Drone => Model;

        /// <summary>
        /// Weight options
        /// </summary>
        public Array WeightOptions { get; } = Enum.GetValues(typeof(WeightCategory));

        /// <summary>
        /// Stations options for the drone to be charged at
        /// </summary>
        public ObservableCollection<int> StationsOptions { get; set; } = new();

        public AddDroneViewModel()
        {
            Load();
            PLNotification.AddHandler<BaseStation>(Load);
        }

        /// <summary>
        /// Add the drone
        /// </summary>
        protected override void Add()
        {
            PLService.AddDrone(Drone);
            Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName());
        }

        /// <summary>
        /// Load stations options
        /// </summary>
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
