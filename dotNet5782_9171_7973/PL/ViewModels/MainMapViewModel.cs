using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    record ItemMarker(Type Type, int Id, MapMarker Marker);

    class MainMapViewModel
    {
        public ObservableCollection<ItemMarker> ItemMarkers { get; set; } = new();

        public RelayCommand LoadCommand { get; set; }

        public MainMapViewModel()
        {
            Load();

            LoadCommand = new(Load);

            PLNotification.BaseStationNotification.AddGlobalHandler(id => ReloadItem(id, PLService.GetBaseStation));
            PLNotification.CustomerNotification.AddGlobalHandler(id => ReloadItem(id, PLService.GetCustomer));
            PLNotification.DroneNotification.AddGlobalHandler(id => ReloadItem(id, PLService.GetDrone));
        }

        private void ReloadItem<T>(int id, Func<int, T> requestFunc) where T: ILocalable
        {
            ItemMarker itemMarker = ItemMarkers.FirstOrDefault(marker => marker.Type == typeof(T) && marker.Id == id);

            if (itemMarker != null)
            {
                ItemMarkers.Remove(itemMarker);
            }

            try
            {
                var item = requestFunc(id);
                ItemMarkers.Add(new(Type: typeof(T), Id: id, MapMarker.FromType(item)));
            }
            catch (BO.ObjectNotFoundException) { }
        }

        private void Load()
        {
            ItemMarkers.Clear();

            // load base stations
            foreach (var station in PLService.GetBaseStationsList().Select(s => PLService.GetBaseStation(s.Id)))
            {
                ItemMarkers.Add(new(Type: typeof(BaseStation), Id: station.Id, Marker: MapMarker.FromType(station)));
            }

            // load customers
            foreach (var customer in PLService.GetCustomersList().Select(c => PLService.GetCustomer(c.Id)))
            {
                ItemMarkers.Add(new(Type: typeof(Customer), Id: customer.Id, Marker: MapMarker.FromType(customer)));
            }

            // load drones
            foreach (var drone in PLService.GetDronesList())
            {
                ItemMarkers.Add(new(Type: typeof(Customer), Id: drone.Id, Marker: MapMarker.FromType(drone)));
            }
        }
    }
}
