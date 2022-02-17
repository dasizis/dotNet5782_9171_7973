using PO;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class DronesListViewModel : QueriableListViewModel<DroneForList>
    {
        /// <summary>
        /// Return an add drone panel
        /// </summary>
        /// <returns>drone panel</returns>
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.DroneView(), Workspace.DronePanelName());
        }

        /// <summary>
        /// Return list of drones
        /// </summary>
        /// <returns>list of <see cref="DroneForList"/></returns>
        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }

        /// <summary>
        /// Return a specific drone
        /// </summary>
        /// <param name="id">id of requested <see cref="DroneForList"/></param>
        /// <returns>the <see cref="DroneForList"/> with the id</returns>
        protected override DroneForList GetItem(int id)
        {
            return PLService.GetDroneForList(id);
        }

        public DronesListViewModel(): base()
        {
            PLNotification.DroneNotification.AddGlobalHandler(ReloadList);
        }
    }
}
