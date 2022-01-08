using PO;
using System;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class FilteredParcelsListViewModel : FilteredListViewModel<ParcelForList>
    {
        public FilteredParcelsListViewModel(Predicate<ParcelForList> predicate) : base(predicate) 
        {
            PLNotification.AddHandler<Parcel>(LoadList);
        }

        protected override void ExecuteOpen(ParcelForList item)
        {
            Views.WorkspaceView.AddPanelCommand.Execute(Workspace.ParcelPanel(item.Id));
        }
        protected override IEnumerable<ParcelForList> GetList()
        {
            return PLService.GetParcelsList();
        }
        protected override void Close()
        {
            Views.WorkspaceView.RemovePanelCommand.Execute(Workspace.ListPanelName(typeof(Parcel)));
        }
    }
}
