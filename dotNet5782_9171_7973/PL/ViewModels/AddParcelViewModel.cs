using PO;
using System;
using System.Collections.ObjectModel;

namespace PL.ViewModels
{
    /// <summary>
    /// A class to represent a view model of Add parcel view
    /// </summary>
    class AddParcelViewModel : AddItemViewModel<ParcelToAdd>
    {
        /// <summary>
        /// the parcel view model's model
        /// </summary>
        public ParcelToAdd Parcel => Model;

        /// <summary>
        /// Priority options
        /// </summary>
        public Array PriorityOptions { get; } = Enum.GetValues(typeof(Priority));

        /// <summary>
        /// Weight options
        /// </summary>
        public Array WeightOptions { get; } = Enum.GetValues(typeof(WeightCategory));

        /// <summary>
        /// Customers options between whom parcel can be delivered 
        /// </summary>
        public ObservableCollection<int> CustomersOptions { get; set; } = new();

        public AddParcelViewModel()
        {
            Load();
            PLNotification.CustomerNotification.AddGlobalHandler(id => Load());
            Parcel.SenderId = PLService.CustomerId;
        }

        /// <summary>
        /// Add the parcel
        /// </summary>
        protected override void Add()
        {
            PLService.AddParcel(Parcel);
            Workspace.RemovePanelCommand.Execute(Workspace.ParcelPanelName());
        }

        /// <summary>
        /// Load customer options
        /// </summary>
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
