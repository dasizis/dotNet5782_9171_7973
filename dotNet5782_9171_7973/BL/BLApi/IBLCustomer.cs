using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi
{
    public interface IBLCustomer
    {
        void AddCustomer(int id, string name, string phone, Location location);
        void UpdateCustomer(int customerId, string name = null, string phone = null);
        IEnumerable<CustomerForList> GetCustomersList();
        Customer GetCustomer(int id);
    }
}
