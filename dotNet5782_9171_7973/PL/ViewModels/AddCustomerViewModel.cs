﻿using PO;

namespace PL.ViewModels
{
    class AddCustomerViewModel : AddItemViewModel<CustomerToAdd>
    {
        public CustomerToAdd Customer => Model;

        protected override void Add()
        {
            PLService.AddCustomer(Customer);
            Workspace.RemovePanelCommand.Execute(Workspace.CustomerPanelName());
        }
    }
}
