using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class DronesListViewModel : ListViewModel<PO.DroneForList>
    {
        public RelayCommand OpenDroneCommand { get; set; }
        public DronesListViewModel(Predicate<PO.DroneForList> predicate) : base(predicate) { }

        protected override void ExecuteOpen(DroneForList item)
        {
            Views.WorkspaceView.AddPanelCommand.Execute(Workspace.ParcelPanel(item.Id));
        }

        protected override IEnumerable<DroneForList> GetList()
        {
            return PLService.GetDronesList();
        }
    }
}
