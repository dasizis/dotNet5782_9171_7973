using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels
{
    class DronesListViewModel : ListViewModel<PO.DroneForList>
    {
        public DronesListViewModel(Predicate<PO.DroneForList> predicate) : base(predicate) { DronesNotification.DronesChangedEvent += LoadList;  }

        protected override void ExecuteOpen(DroneForList item)
        {
            Views.WorkspaceView.AddPanelCommand.Execute(Workspace.DronePanel(item.Id));
        }

        protected override void Close()
        {
            Views.WorkspaceView.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Drone)));
        }

        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }
    }
}
