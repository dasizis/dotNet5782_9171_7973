using PO;
using System;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class ParcelsListViewModel : QueriableListViewModel<ParcelForList>
    {
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.ParcelView(), Workspace.ParcelPanelName());
        }

        protected override IEnumerable<ParcelForList> GetList()
        {
            return PLService.GetParcelsList();
        }

        public ParcelsListViewModel() : base()
        {
            PLNotification.AddHandler<Parcel>(ReloadList);
        }
    }
}
