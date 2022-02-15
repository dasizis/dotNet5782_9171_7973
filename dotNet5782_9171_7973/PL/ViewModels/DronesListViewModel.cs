using PO;
using System;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class DronesListViewModel : QueriableListViewModel<DroneForList>
    {
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.DroneView(), Workspace.DronePanelName());
        }

        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }

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
