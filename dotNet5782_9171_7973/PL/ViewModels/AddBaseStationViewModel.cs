using PO;

namespace PL.ViewModels
{
    class AddBaseStationViewModel : AddItemViewModel<BaseStationToAdd>
    {
        public BaseStationToAdd BaseStation => Model;

        protected override void Add()
        {
            PLService.AddBaseStation(BaseStation);
            Workspace.RemovePanelCommand.Execute(Workspace.BaseStationPanelName());
        }
    }
}
