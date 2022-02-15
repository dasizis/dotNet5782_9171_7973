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
        /// <param name="longitude">The customer location longitude</param>
        /// <param name="latitude">The customer location latitude</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        void AddCustomer(int id, string name, string phone, double longitude, double latitude);

        /// <summary>
        /// return specific customer
        /// </summary>
        /// <param name="id">Id of requested customer</param>
        /// <returns>The <see cref="Customer"/> who has the spesific Id</returns>
        /// <exception cref="ObjectNotFoundException" />
        Customer GetCustomer(int id);

        /// <summary>
        /// Returns specific customer for list
        /// </summary>
        /// <param name="id">id of requested customer</param>
        /// <returns>customer with id</returns>
        /// <exception cref="ObjectNotFoundException" />
        CustomerForList GetCustomerForList(int id);

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
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        void DeleteCustomer(int customerId);
    }
}
