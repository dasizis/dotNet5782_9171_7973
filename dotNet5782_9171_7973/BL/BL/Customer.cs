using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL
    {
        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="id">the customer id</param>
        /// <param name="name">the customer name</param>
        /// <param name="phone">the customer phone number</param>
        /// <param name="location">the customer location</param>
        public void AddCustomer(int id, string name, string phone, Location location)
        {
            var customer = new Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Location = location,
                Send = new(),
                Recieve = new(),
            };

            try
            {
                dal.Add(
                    new DO.Customer()
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Phone = customer.Phone,
                        Longitude = customer.Location.Longitude,
                        Latitude = customer.Location.Latitude,
                    }
               );
            }
            catch
            {
                throw new IdAlreadyExistsException(typeof(Customer), id);
            }

        }
        /// <summary>
        /// return customers list
        /// </summary>
        /// <returns>customers list</returns>
        public IEnumerable<CustomerForList> GetCustomersList()
        {
            return from customer in  dal.GetList<DO.Customer>()
                   select GetCustomerForList(customer.Id);
        }
        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId">customer to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        public void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            DO.Customer customer;

            try
            {
                customer = dal.GetById<DO.Customer>(customerId);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), customerId);
            }

            dal.Update<DO.Customer>(customer.Id, nameof(customer.Name), name ?? customer.Name);
            dal.Update<DO.Customer>(customer.Id, nameof(customer.Phone), phone ?? customer.Phone);
        }
    }
}
