using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

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
                    new IDAL.DO.Customer()
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
            return dal.GetList<IDAL.DO.Customer>().Select(customer => GetCustomerForList(customer.Id));
        }
        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId">customer to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        public void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            IDAL.DO.Customer customer;

            try
            {
                customer = dal.GetById<IDAL.DO.Customer>(customerId);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), customerId);
            }

            dal.Remove<IDAL.DO.Customer>(customer.Id);

            customer.Name = name ?? customer.Name;
            customer.Phone = phone ?? customer.Phone;

            dal.Add(customer);
        }
    }
}
