using PO;
using System.Collections.Generic;

namespace PL.ViewModels
{
    /// <summary>
    /// A class to represent base stations list view model
    /// </summary>
    class BaseStationListViewModel : QueriableListViewModel<BaseStationForList>
    {
        /// <summary>
        /// Return an add base station panel
        /// </summary>
        /// <returns>base station panel</returns>
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.BaseStationView(), Workspace.BaseStationPanelName());
        }

        /// <summary>
        /// Return list of base stations
        /// </summary>
        /// <returns>list of base stations</returns>
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
