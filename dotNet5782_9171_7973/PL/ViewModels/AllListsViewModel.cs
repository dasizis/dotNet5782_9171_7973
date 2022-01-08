using PO;
using System.Collections.ObjectModel;

namespace PL.ViewModels
{
    class AllListsViewModel
    {
        public ObservableCollection<CustomerForList> Customers { get; set; } = new();
        public ObservableCollection<DroneForList> Drones { get; set; } = new();
        public ObservableCollection<BaseStationForList> BaseStations { get; set; } = new();
        public ObservableCollection<ParcelForList> Parcels { get; set; } = new();

        public RelayCommand AddDroneCommand { get; set; }
        public RelayCommand AddParcelCommand { get; set; }
        public RelayCommand AddStationCommand { get; set; }
        public RelayCommand AddCustomerCommand { get; set; }

        public AllListsViewModel()
        {
            LoadCustomers();
            LoadDrones();
            LoadBaseStations();
            LoadParcels();

            PLNotification.AddHandler<BaseStation>(LoadBaseStations);
            PLNotification.AddHandler<Customer>(LoadCustomers);
            PLNotification.AddHandler<Drone>(LoadDrones);
            PLNotification.AddHandler<Parcel>(LoadParcels);

            AddDroneCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.DronePanel()));
            AddParcelCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.ParcelPanel()));
            AddStationCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.BaseStationPanel()));
            AddCustomerCommand = new(() =>Workspace.AddPanelCommand.Execute(Workspace.CustomerPanel()));
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
