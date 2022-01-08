using PO;
using System;
using System.Collections.Generic;

namespace PL.ViewModels
{
    //class DronesListViewModel : ListViewModel<PO.DroneForList>
    //{
    //    public DronesListViewModel(Predicate<PO.DroneForList> predicate) : base(predicate) { DronesNotification.DronesChangedEvent += LoadList;  }

    //    protected override void ExecuteOpen(DroneForList item)
    //    {
    //        Views.WorkspaceView.AddPanelCommand.Execute(Workspace.DronePanel(item.Id));
    //    }

    //    protected override void Close()
    //    {
    //        Views.WorkspaceView.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Drone)));
    //    }

    //    protected override IEnumerable<DroneForList> GetList()
    //    {
    //        return PLService.GetDronesList();
    //    }
    //}

    class DronesListViewModel : QueriableListViewModel<DroneForList>
    {
        public RelayCommand<object> DeleteItemCommand { get; set; }
        //I changed here to make it work
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.AddDroneView(), Workspace.AddDronePanelName());
        }

        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }

        public DronesListViewModel(): base()
        {
            DronesNotification.DronesChangedEvent += ReloadList;
            DeleteItemCommand = new((e) => Delete(e));
        }

        private void Delete(object item)
        {    
           PLService.DeleteDrone((item as DroneForList).Id);
        }

    }
}
