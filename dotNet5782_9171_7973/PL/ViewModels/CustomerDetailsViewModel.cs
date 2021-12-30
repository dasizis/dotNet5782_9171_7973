﻿using PO;
using System.Windows;

namespace PL.ViewModels
{
    class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand OpenSentParcelsListCommand { get; set; }
        public RelayCommand OpenRecievedParcelsListCommand { get; set; }

        public CustomerDetailsViewModel(int id)
        {
            Customer = PLService.GetCustomer(id);

            CustomersNotification.CustomersChanged += LoadCustomer;

            DeleteCommand = new(Delete, CanBeDeleted);
            UpdateCommand = new(Update, () => Customer.Error == null);
            //TODO
            OpenSentParcelsListCommand = new(() => Views.WorkspaceView.AddPanelCommand.Execute(Workspace.ParcelsListPanel((p) => p.SenderName == Customer.Name, "A")),
                                    () => Customer.Send.Count != 0);
            OpenRecievedParcelsListCommand = new(() => Views.WorkspaceView.AddPanelCommand.Execute(Workspace.ParcelsListPanel((p) => p.TargetName == Customer.Name, "B")),
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
            CustomersNotification.CustomersChanged -= LoadCustomer;

            PLService.DeleteCustomer(Customer.Id);
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
        }
    }
}
