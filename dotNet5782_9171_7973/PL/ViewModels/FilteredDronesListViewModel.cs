using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class FilteredDronesListViewModel : FilteredListViewModel<DroneForList>
    {
        public FilteredDronesListViewModel(Predicate<DroneForList> predicate) : base(predicate) { DronesNotification.DronesChangedEvent += LoadList; }

        protected override void ExecuteOpen(DroneForList item)
        {
            Views.WorkspaceView.AddPanelCommand.Execute(Workspace.DronePanel(item.Id));
        }
        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }
        protected override void Close()
        {
            Views.WorkspaceView.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Drone)));
        }
    }
}
