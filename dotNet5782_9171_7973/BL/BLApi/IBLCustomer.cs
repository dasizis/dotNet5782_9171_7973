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
        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="id">the customer id</param>
        /// <param name="name">the customer name</param>
        /// <param name="phone">the customer phone number</param>
        /// <param name="location">the customer location</param>
        void AddCustomer(int id, string name, string phone, Location location);

        /// <summary>
        /// return specific customer
        /// </summary>
        /// <param name="id">id of requested customer</param>
        /// <returns>customer with id</returns>
        Customer GetCustomer(int id);
        
        /// <summary>
        /// return customers list
        /// </summary>
        /// <returns>customers list</returns>
        IEnumerable<CustomerForList> GetCustomersList();

        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId">customer to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        void UpdateCustomer(int customerId, string name = null, string phone = null);

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        void DeleteCustomer(int customerId);
    }
}
