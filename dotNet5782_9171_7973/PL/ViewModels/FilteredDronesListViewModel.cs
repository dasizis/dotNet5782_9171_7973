using PO;
using System;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class FilteredDronesListViewModel : FilteredListViewModel<DroneForList>
    {
        public FilteredDronesListViewModel(Predicate<DroneForList> predicate) : base(predicate) 
        { 
            PLNotification.DroneNotification.AddGlobalHandler(LoadList); 
        }

        /// <summary>
        /// Open a drone from the list
        /// </summary>
        /// <param name="item">drone to open</param>
        protected override void ExecuteOpen(DroneForList item)
        {
            Workspace.AddPanelCommand.Execute(Workspace.DronePanel(item.Id));
        }

        /// <summary>
        /// Get list of drones
        /// </summary>
        /// <returns>list of <see cref="DroneForList"/></returns>
        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }

        /// <summary>
        /// Close the filtered drones panel
        /// </summary>
        protected override void Close()
        {
            Workspace.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Drone)));
        }

        /// <summary>
        /// Get a spesific drone
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>the <see cref="DroneForList"/> with the id</returns>
        protected override DroneForList GetItem(int id)
        {
            return PLService.GetDroneForList(id);
        }
    }
}
