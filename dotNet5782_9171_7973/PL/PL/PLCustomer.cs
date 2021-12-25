using System.Collections.Generic;

namespace PL
{
    public static partial class PL
    {
        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="cutomer">The customer to add</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        public static void AddCustomer(CustomerToAdd cutomer)
        {
            CustomersNotification.NotifyCustomerChanged();
        }

        /// <summary>
        /// return specific customer
        /// </summary>
        /// <param name="id">Id of requested customer</param>
        /// <returns>The <see cref="Customer"/> who has the spesific Id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static Customer GetCustomer(int id);

        /// <summary>
        /// return customers list
        /// </summary>
        /// <returns>customers list</returns>
        public static IEnumerable<CustomerForList> GetCustomersList();

        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId">customer to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        public static void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            bl.UpdateCustomer(customerId, name, phone);
            CustomersNotification.NotifyCustomerChanged();
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void DeleteCustomer(int customerId)
        {
            bl.DeleteCustomer(customerId);
            CustomersNotification.NotifyCustomerChanged();
        }
    }
}
