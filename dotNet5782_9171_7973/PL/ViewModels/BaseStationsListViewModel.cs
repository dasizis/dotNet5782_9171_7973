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
        /// <returns>list of <see cref="BaseStationForList"/></returns>
        protected override IEnumerable<BaseStationForList> GetList()
        {
            return PLService.GetBaseStationsList();
        }

        /// <summary>
        /// Return a specific base station
        /// </summary>
        /// <param name="id">id of requested <see cref="BaseStationForList"/></param>
        /// <returns>the <see cref="BaseStationForList"/> with the id</returns>
        protected override BaseStationForList GetItem(int id)
        {
            return PLService.GetBaseStationForList(id);
        }

        public BaseStationListViewModel() : base()
        {
            PLNotification.BaseStationNotification.AddGlobalHandler(ReloadList);
        }
    }
}
