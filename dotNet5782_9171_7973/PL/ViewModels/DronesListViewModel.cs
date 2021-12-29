using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class DronesListViewModel
    {
        public int Id { get; set; }
        List<PO.Drone> drones;
        public DronesListViewModel(List<PO.Drone> droneslist)
        {
            drones = droneslist;
            Id = drones.First().Id;
        }
    }
}
