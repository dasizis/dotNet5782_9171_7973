using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class DronesListViewModel
    {
        public int SelectedItem { get; set; }
        public List<PO.Drone> Drones { get; set; }
        public RelayCommand OpenDroneCommand { get; set; }
        public DronesListViewModel(List<PO.Drone> droneslist)
        {
            Drones = droneslist.ToList();
            OpenDroneCommand = new(() => Views.WorkspaceView.AddPanelCommand.Execute(Workspace.DronePanel()),
                                    () => true);
        }
    }   

}
