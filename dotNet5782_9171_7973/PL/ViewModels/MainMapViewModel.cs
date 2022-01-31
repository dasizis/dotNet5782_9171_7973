using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class MainMapViewModel
    {
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        public RelayCommand LoadCommand { get; set; }

        public MainMapViewModel()
        {
            Load();

            LoadCommand = new(Load);
        }

        private void Load()
        {
            Markers.Clear();

            IEnumerable<ILocalable> list = PLService.GetCustomersList().Select(c => PLService.GetCustomer(c.Id)).Cast<ILocalable>()
                                                    .Union(PLService.GetDronesList().Select(d => PLService.GetDrone(d.Id)))
                                                    .Union(PLService.GetBaseStationsList().Select(b => PLService.GetBaseStation(b.Id)));

            foreach (var item in list)
            {
                Markers.Add(MapMarker.FromType(item));
            }
        }
    }
}
