using PO;
using System;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class ParcelsListViewModel : QueriableListViewModel<ParcelForList>
    {
        /// <summary>
        /// Return an add parcel panel
        /// </summary>
        /// <returns>parcel panel</returns>
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.ParcelView(), Workspace.ParcelPanelName());
        }

        /// <summary>
        /// Return list of parcels
        /// </summary>
        /// <returns>list of <see cref="ParcelForList"/></returns>
        protected override IEnumerable<ParcelForList> GetList()
        {
            return PLService.GetParcelsList();
        }

        /// <summary>
        /// Return a specific parcel
        /// </summary>
        /// <param name="id">id of requested <see cref="ParcelForList"/></param>
        /// <returns>the <see cref="ParcelForList"/> with the id</returns>
        protected override ParcelForList GetItem(int id)
        {
            return PLService.GetParcelForList(id);
        }

        public ParcelsListViewModel() : base()
        {
            PLNotification.ParcelNotification.AddGlobalHandler(ReloadList);
        }
    }
}
