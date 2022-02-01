using PO;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL.ViewModels
{
    class CustomerDetailsViewModel
    {
        /// <summary>
        /// The customer
        /// </summary>
        public Customer Customer { get; set; } = new();

        /// <summary>
        /// List of markers to display customer's map
        /// </summary>
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        /// <summary>
        /// Delete customer command
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Update details of customer command
        /// </summary>
        public RelayCommand UpdateCommand { get; set; }

        /// <summary>
        /// Open list of parcels the customer sent command
        /// </summary>
        public RelayCommand OpenSentParcelsListCommand { get; set; }

        /// <summary>
        /// Open list of parcels the customer Recieved command
        /// </summary>
        public RelayCommand OpenRecievedParcelsListCommand { get; set; }

        public CustomerDetailsViewModel(int id)
        {
            Customer.Id = id;
            LoadCustomer();

            PLNotification.CustomerNotification.AddHandler(LoadCustomer, id);

            DeleteCommand = new(Delete, CanBeDeleted);
            UpdateCommand = new(Update, () => Customer.Error == null);

            OpenSentParcelsListCommand = new(
                () => Workspace.AddPanelCommand.Execute(Workspace.ParcelsListPanel(p => p.SenderName == Customer.Name, 
                                                                                   Workspace.CustomerSentListName(Customer.Id))),
                () => Customer.Send.Count != 0
            );

            OpenRecievedParcelsListCommand = new(
                () => Workspace.AddPanelCommand.Execute(Workspace.ParcelsListPanel(p => p.TargetName == Customer.Name, 
                                                                                   Workspace.CustomerRecievedListName(Customer.Id))),
                () => Customer.Recieve.Count != 0
            );
        }

        /// <summary>
        /// Update customer's details
        /// </summary>
        private void Update()
        {
            PLService.UpdateCustomer(Customer.Id, Customer.Name, Customer.Phone);
            MessageBox.Show($"Customer {Customer.Name} updated");
        }

        /// <summary>
        /// Return whether customer can be deleted or not
        /// </summary>
        /// <returns>whether customer can be deleted or not</returns>
        private bool CanBeDeleted()
        {
            return Customer.Send.Count == 0 && Customer.Recieve.Count == 0;
        }

        /// <summary>
        /// Delete customer
        /// </summary>
        private void Delete()
        {
            PLService.DeleteCustomer(Customer.Id);
            MessageBox.Show($"Customer {Customer.Name} deleted");
        }

        /// <summary>
        /// Load customer 
        /// </summary>
        private void LoadCustomer()
        {
            Customer.Reload(PLService.GetCustomer(Customer.Id));
            Markers.Clear();
            Markers.Add(MapMarker.FromType(Customer));
        }
    }
}
