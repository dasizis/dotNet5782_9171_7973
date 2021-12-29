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
        public ObservableCollection<object> Customers { get; set; } = new();
        public ObservableCollection<object> Drones { get; set; } = new();
        public ObservableCollection<object> BaseStations { get; set; } = new();

        public MainMapViewModel()
        {
            LoadCustomers();
            LoadDrones();
            LoadBaseStations();

            DronesNotification.DronesChangedEvent += LoadDrones;
            CustomersNotification.CustomersChanged += LoadCustomers;
            BaseStationsNotification.BaseStationsChangedEvent += LoadBaseStations;
        }

        void LoadCustomers()
        {
            Customers.Clear();

            foreach (var customer in PLService.GetCustomersList().Select(c => PLService.GetCustomer(c.Id)))
            {
                Customers.Add(customer);
            }
        }

        void LoadDrones()
        {
            Drones.Clear();

            foreach (var drone in PLService.GetDronesList().Select(d => PLService.GetDrone(d.Id)))
            {
                Drones.Add(drone);
            }
        }

        void LoadBaseStations()
        {
            BaseStations.Clear();

            foreach (var baseStation in PLService.GetBaseStationsList().Select(b => PLService.GetBaseStation(b.Id)))
            {
                BaseStations.Add(baseStation);
            }
        }
    }
}
