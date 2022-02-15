using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    record ExtendedMarker(Type Type, int Id, double Longitude, double Latitude, string Color, string Label);

    class MainMapViewModel
    {
        public ObservableCollection<ExtendedMarker> ItemMarkers { get; set; } = new();

        public RelayCommand LoadCommand { get; set; }

        public MainMapViewModel()
        {
            Load();

            LoadCommand = new(Load);

            PLNotification.BaseStationNotification.AddGlobalHandler(id => ReloadItem(id, PLService.GetBaseStation));
            PLNotification.CustomerNotification.AddGlobalHandler(id => ReloadItem(id, PLService.GetCustomer));
            PLNotification.DroneNotification.AddGlobalHandler(id => ReloadItem(id, PLService.GetDrone));
        }

        private void ReloadItem<T>(int id, Func<int, T> requestFunc) where T: ILocalable, IIdentifiable
        {
            ExtendedMarker itemMarker = ItemMarkers.FirstOrDefault(marker => marker.Type == typeof(T) && marker.Id == id);

            if (itemMarker != null)
            {
                ItemMarkers.Remove(itemMarker);
            }

            try
            {
                var item = requestFunc(id);
                ItemMarkers.Add(CreateExtendedMarker(item));
            }
            catch (BO.ObjectNotFoundException) { }
        }

        private ExtendedMarker CreateExtendedMarker<T>(T item) where T : ILocalable, IIdentifiable
        {
            MapMarker marker = MapMarker.FromType(item);
            return new(
                Type: typeof(T),
                Id: item.Id,
                Longitude: marker.Longitude,
                Latitude: marker.Latitude,
                Color: marker.Color,
                Label: marker.Label
            );
        }

        private void Load()
        {
            ItemMarkers.Clear();

            // load base stations
            foreach (var station in PLService.GetBaseStationsList().Select(s => PLService.GetBaseStation(s.Id)))
            {
                ItemMarkers.Add(CreateExtendedMarker(station));
            }

            // load customers
            foreach (var customer in PLService.GetCustomersList().Select(c => PLService.GetCustomer(c.Id)))
            {
                ItemMarkers.Add(CreateExtendedMarker(customer));
            }

            // load drones
            foreach (var drone in PLService.GetDronesList())
            {
                ItemMarkers.Add(CreateExtendedMarker(drone));
            }
        }
    }
}
