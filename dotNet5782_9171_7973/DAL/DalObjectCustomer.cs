using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// adds a customer to customers list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="phone"></param>
        public void AddCustomer(int id, string name, double longitude, double latitude, string phone)
        {
            if (DataSource.customers.DoesIdExist(id))
            {
                throw new IdAlreadyExistsException(typeof(Customer), id);
            }

            Customer customer = new Customer()
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                Phone = phone,
            };

            DataSource.customers.Add(customer);
        }
        
        /// <summary>
        /// returns the customers list
        /// </summary>
        /// <returns>list of all the exist customers</returns>
        public IEnumerable<Customer> GetCustomersList()
        {
            return DataSource.customers;
        }
    }
}
