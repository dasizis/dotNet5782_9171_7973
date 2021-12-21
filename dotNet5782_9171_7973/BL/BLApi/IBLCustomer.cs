using System.Collections.Generic;
using BO;

namespace BLApi
{
    /// <summary>
    /// Declares all <see cref="IBL"/> methods related to <see cref="Customer"/>
    /// </summary>
    public interface IBLCustomer
    {
        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="id">the customer id</param>
        /// <param name="name">the customer name</param>
        /// <param name="phone">the customer phone number</param>
        /// <param name="location">the customer location</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        void AddCustomer(int id, string name, string phone, Location location);

        /// <summary>
        /// return specific customer
        /// </summary>
        /// <param name="id">Id of requested customer</param>
        /// <returns>The <see cref="Customer"/> who has the spesific Id</returns>
        /// <exception cref="ObjectNotFoundException" />
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
