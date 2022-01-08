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

        public DronesListViewModel(): base()
        {
            PLNotification.AddHandler<Drone>(ReloadList);
        }
    }
}
