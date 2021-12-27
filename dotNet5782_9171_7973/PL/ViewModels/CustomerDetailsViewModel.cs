using PO;
using System.Windows;

namespace PL.ViewModels
{
    class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public bool CanBeDeleted { get; set; }

        public CustomerDetailsViewModel(int id)
        {
            Customer = PLService.GetCustomer(id);

            CustomersNotification.CustomersChanged += LoadCustomer;

            DeleteCommand = new(Delete, () => CanBeDeleted);
            UpdateCommand = new(Update, () => Customer.Error == null);

            Customer.Id = id;
            LoadCustomer();
        }

        private void Update()
        {
            PLService.UpdateCustomer(Customer.Id, Customer.Name, Customer.Phone);
            MessageBox.Show($"Customer {Customer.Name} updated");
        }

        private void Delete()
        {
            PLService.DeleteDrone(Customer.Id);
            MessageBox.Show($"Customer {Customer.Name} deleted");
            // Close Tab 
        }

        private void LoadCustomer()
        {
            string name = Customer.Name;
            string phone = Customer.Phone;
            Customer.Reload(PLService.GetCustomer(Customer.Id));
            Customer.Name = name;
            Customer.Phone = phone;

            CanBeDeleted = true;/*How To check it?*/
        }
    }
}
