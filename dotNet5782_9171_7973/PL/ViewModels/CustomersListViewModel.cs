using PO;
using System.Collections.Generic;

namespace PL.ViewModels
{
    class CustomersListViewModel : QueriableListViewModel<CustomerForList>
    {
        /// <summary>
        /// Return an add customer panel
        /// </summary>
        /// <returns>customer panel</returns>
        protected override Panel GetAddPanel()
        {
            return new Panel(PanelType.Add, new Views.CustomerView(), Workspace.CustomerPanelName());
        }

        /// <summary>
        /// Return list of customers
        /// </summary>
        /// <returns>list of <see cref="CustomerForList"/></returns>
        protected override IEnumerable<CustomerForList> GetList()
        {
            return PLService.GetCustomersList();
        }

        /// <summary>
        /// Return a specific customer
        /// </summary>
        /// <param name="id">id of requested <see cref="CustomerForList"/></param>
        /// <returns>the <see cref="CustomerForList"/> with the id</returns>
        protected override CustomerForList GetItem(int id)
        {
            return PLService.GetCustomerForList(id);
        }

        public CustomersListViewModel() : base()
        {
            PLNotification.CustomerNotification.AddGlobalHandler(ReloadList);
        }
    }
}
