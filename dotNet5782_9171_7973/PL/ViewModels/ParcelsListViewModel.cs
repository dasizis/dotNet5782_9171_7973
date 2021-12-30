using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class ParcelsListViewModel : ListViewModel<ParcelForList>
    {
        public ParcelsListViewModel(Predicate<ParcelForList> predicate) : base(predicate) { ParcelsNotification.ParcelsChangedEvent += LoadList; }

        protected override void ExecuteOpen(ParcelForList item)
        {
            Views.WorkspaceView.AddPanelCommand.Execute(Workspace.ParcelPanel(item.Id));
        }

        protected override IEnumerable<ParcelForList> GetList()
        {
            return PLService.GetParcelsList();
        }
    }
}
