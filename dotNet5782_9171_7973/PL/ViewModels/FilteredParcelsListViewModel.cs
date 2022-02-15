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

        protected override void ExecuteOpen(ParcelForList item)
        {
            Workspace.AddPanelCommand.Execute(Workspace.ParcelPanel(item.Id));
        }
        protected override IEnumerable<ParcelForList> GetList()
        {
            return PLService.GetParcelsList();
        }
        protected override void Close()
        {
            Workspace.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Parcel)));
        }

        protected override ParcelForList GetItem(int id)
        {
            return PLService.GetParcelForList(id);
        }
    }
}
