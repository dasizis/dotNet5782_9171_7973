using PO;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class BaseStationListViewModel : QueriableListViewModel<BaseStationForList>
    {
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.BaseStationView(), Workspace.BaseStationPanelName());
        }

        protected override IEnumerable<BaseStationForList> GetList()
        {
            return PLService.GetBaseStationsList();
        }

        public BaseStationListViewModel() : base()
        {
            PLNotification.AddHandler<BaseStation>(ReloadList);
        }
    }
}
