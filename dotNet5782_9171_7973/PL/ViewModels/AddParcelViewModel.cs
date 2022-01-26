using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PO;

namespace PL.ViewModels
{
    class AddParcelViewModel : AddItemViewModel<ParcelToAdd>
    {
        public ParcelToAdd Parcel => Model;

        public Array PriorityOptions { get; } = Enum.GetValues(typeof(Priority));

        public Array WeightOptions { get; } = Enum.GetValues(typeof(WeightCategory));

        public ObservableCollection<int> CustomersOptions { get; set; } = new();

        public AddParcelViewModel()
        {
            Load();
            PLNotification.AddHandler<Customer>(Load);
        }

        protected override void Add()
        {
            PLService.AddParcel(Parcel);
            Workspace.RemovePanelCommand.Execute(Workspace.ParcelPanelName());
        }

        void Load()
        {
            CustomersOptions.Clear();

            foreach (var customer in PLService.GetCustomersList())
            {
                CustomersOptions.Add(customer.Id);
            }
        }
    }
}
