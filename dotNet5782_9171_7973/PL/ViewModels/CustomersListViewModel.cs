using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    class CustomersListViewModel : QueriableListViewModel<CustomerForList>
    {
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.ParcelView(), Workspace.ParcelPanelName());
        }

        protected override IEnumerable<CustomerForList> GetList()
        {
            return PLService.GetCustomersList();
        }

        public CustomersListViewModel() : base()
        {
            PLNotification.AddHandler<Customer>(ReloadList);
        }
    }
}
