using PO;
using System;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class FilteredParcelsListViewModel : FilteredListViewModel<ParcelForList>
    {
        public FilteredParcelsListViewModel(Predicate<ParcelForList> predicate) : base(predicate) 
        {
            PLNotification.ParcelNotification.AddGlobalHandler(LoadList);
        }

        /// <summary>
        /// Open a parcel from the list
        /// </summary>
        /// <param name="item">parcel to open</param>
        protected override void ExecuteOpen(ParcelForList item)
        {
            Workspace.AddPanelCommand.Execute(Workspace.ParcelPanel(item.Id));
        }

        /// <summary>
        /// Get list of parcels
        /// </summary>
        /// <returns>list of <see cref="ParcelForList"/></returns>
        protected override IEnumerable<ParcelForList> GetList()
        {
            return PLService.GetParcelsList();
        }

        /// <summary>
        /// Close the filtered parcels panel
        /// </summary>
        protected override void Close()
        {
            Workspace.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Parcel)));
        }

        /// <summary>
        /// Get a spesific parcel
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>the <see cref="ParcelForList"/> with the id</returns>
        protected override ParcelForList GetItem(int id)
        {
            return PLService.GetParcelForList(id);
        }
    }
}
