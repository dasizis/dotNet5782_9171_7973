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
        public FilteredDronesListViewModel(Predicate<DroneForList> predicate) : base(predicate) 
        { 
            PLNotification.DroneNotification.AddGlobalHandler(LoadList); 
        }

        protected override void ExecuteOpen(DroneForList item)
        {
            Workspace.AddPanelCommand.Execute(Workspace.DronePanel(item.Id));
        }
        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }
        protected override void Close()
        {
            Workspace.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Drone)));
        }

        protected override DroneForList GetItem(int id)
        {
            return PLService.GetDroneForList(id);
        }
    }
}
