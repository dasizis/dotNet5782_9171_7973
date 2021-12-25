using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class AddCustomerViewModel : AddItemViewModel<CustomerToAdd>
    {
        public CustomerToAdd Customer => Model;

        protected override void Add()
        {
            PL.AddCustomer(Customer);
        }
    }
}
