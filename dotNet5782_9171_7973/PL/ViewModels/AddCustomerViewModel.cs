using PO;

namespace PL.ViewModels
{
    /// <summary>
    /// A class to represent a view model of Add customer view
    /// </summary>
    class AddCustomerViewModel : AddItemViewModel<CustomerToAdd>
    {
        /// <summary>
        /// the customer view model's model
        /// </summary>
        public CustomerToAdd Customer => Model;

        /// <summary>
        /// Add the customer
        /// </summary>
        protected override void Add()
        {
            PLService.AddCustomer(Customer);
            Workspace.RemovePanelCommand.Execute(Workspace.CustomerPanelName());
        }
    }
}
