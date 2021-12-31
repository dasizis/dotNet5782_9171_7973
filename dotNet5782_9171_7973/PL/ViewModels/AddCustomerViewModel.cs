using PO;

namespace PL.ViewModels
{
    class AddCustomerViewModel : AddItemViewModel<CustomerToAdd>
    {
        public CustomerToAdd Customer => Model;

        protected override void Add()
        {
            PLService.AddCustomer(Customer);
            Views.WorkspaceView.RemovePanelCommand.Execute(Workspace.AddCustomerPanelName());
        }
    }
}
