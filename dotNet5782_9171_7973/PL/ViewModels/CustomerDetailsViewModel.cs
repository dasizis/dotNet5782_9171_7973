using PO;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL.ViewModels
{
    class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; } = new();
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand OpenSentParcelsListCommand { get; set; }
        public RelayCommand OpenRecievedParcelsListCommand { get; set; }

        public CustomerDetailsViewModel(int id)
        {
            Customer.Id = id;
            LoadCustomer();

            PLNotification.AddHandler<Customer>(LoadCustomer, id);

            DeleteCommand = new(Delete, CanBeDeleted);
            UpdateCommand = new(Update, () => Customer.Error == null);

            OpenSentParcelsListCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.ParcelsListPanel((p) => p.SenderName == Customer.Name, Workspace.CustomerSentListName(Customer.Id))),
                                    () => Customer.Send.Count != 0);
            OpenRecievedParcelsListCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.ParcelsListPanel((p) => p.TargetName == Customer.Name, Workspace.CustomerRecievedListName(Customer.Id))),
                                    () => Customer.Recieve.Count != 0);
        }

        private void Update()
        {
            PLService.UpdateCustomer(Customer.Id, Customer.Name, Customer.Phone);
            MessageBox.Show($"Customer {Customer.Name} updated");
        }

        private bool CanBeDeleted()
        {
            return Customer.Send.TrueForAll(parcel => parcel.Supplied != null)
                   && Customer.Recieve.TrueForAll(parcel => parcel.Supplied != null);
        }

        private void Delete()
        {
            PLService.DeleteCustomer(Customer.Id);
            MessageBox.Show($"Customer {Customer.Name} deleted");
            // Close Tab 
        }

        private void LoadCustomer()
        {
            Customer.Reload(PLService.GetCustomer(Customer.Id));
            Markers.Clear();
            Markers.Add(MapMarker.FromType(Customer));
        }
    }
}
