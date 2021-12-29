using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class AllListsViewModel
    {
        public ObservableCollection<CustomerForList> Customers { get; set; } = new();
        public ObservableCollection<DroneForList> Drones { get; set; } = new();
        public ObservableCollection<BaseStationForList> BaseStations { get; set; } = new();
        public ObservableCollection<ParcelForList> Parcels { get; set; } = new();

        public AllListsViewModel()
        {
            LoadCustomers();
            LoadDrones();
            LoadBaseStations();
            LoadParcels();

            DronesNotification.DronesChangedEvent += LoadDrones;
            CustomersNotification.CustomersChanged += LoadCustomers;
            BaseStationsNotification.BaseStationsChangedEvent += LoadBaseStations;
            ParcelsNotification.ParcelsChangedEvent += LoadParcels;
        }

        void LoadCustomers()
        {
            Customers.Clear();

            foreach (var customer in PLService.GetCustomersList())
            {
                Customers.Add(customer);
            }
        }

        void LoadDrones()
        {
            Drones.Clear();

            foreach (var drone in PLService.GetDronesList())
            {
                Drones.Add(drone);
            }
        }

        void LoadBaseStations()
        {
            BaseStations.Clear();

            foreach (var baseStation in PLService.GetBaseStationsList())
            {
                BaseStations.Add(baseStation);
            }
        }

        void LoadParcels()
        {
            Parcels.Clear();

            foreach (var parcel in PLService.GetParcelsList())
            {
                Parcels.Add(parcel);
            }
        }
    }
}
