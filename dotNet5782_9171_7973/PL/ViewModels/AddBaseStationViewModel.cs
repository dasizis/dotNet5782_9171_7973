using PO;

namespace PL.ViewModels
{
    class AddBaseStationViewModel : AddItemViewModel<BaseStationToAdd>
    {
        public BaseStationToAdd BaseStation => Model;

        protected override void Add()
        {
            PLService.AddBaseStation(BaseStation);
            Views.WorkspaceView.RemovePanelCommand.Execute(Workspace.AddBaseStationPanelName());
        }
    }
}
