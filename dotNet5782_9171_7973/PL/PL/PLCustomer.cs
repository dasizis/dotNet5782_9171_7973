using System.Collections.Generic;
using System.Linq;
using PO;

namespace PL
{
    static partial class PLService
    {
        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="customer">The customer to add</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        public static void AddCustomer(CustomerToAdd customer)
        {
            bl.AddCustomer((int)customer.Id,
                           customer.Name,
                           customer.Phone,
                           customer.Mail,
                           double.Parse(customer.Longitude),
                           double.Parse(customer.Latitude));

            PLNotification.CustomerNotification.NotifyItemChanged((int)customer.Id);
        }

        /// <summary>
        /// return specific customer
        /// </summary>
        /// <param name="id">Id of requested customer</param>
        /// <returns>The <see cref="Customer"/> who has the spesific Id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static Customer GetCustomer(int id)
        {
            BO.Customer customer = bl.GetCustomer(id);

            List<Parcel> send = new();
            foreach(var parcel in customer.Send)
            {
                send.Add(GetParcel(parcel.Id));
            }

            List<Parcel> recieve = new();
            foreach (var parcel in customer.Recieve)
            {
                recieve.Add(GetParcel(parcel.Id));
            }

            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Mail = customer.Mail,
                Location = new Location()
                { 
                    Latitude = customer.Location.Latitude,
                    Longitude = customer.Location.Longitude 
                },
                Send = send,
                Recieve = recieve,
            };
        }

        /// <summary>
        /// return customers list
        /// </summary>
        /// <returns>customers list</returns>
        public static IEnumerable<CustomerForList> GetCustomersList()
        {
            return bl.GetCustomersList().Select(customer => ConvertCustomer(customer));
        }

        /// <summary>
        /// Converts <see cref="BO.CustomerForList"/> to <see cref="CustomerForList"/>
        /// </summary>
        /// <param name="customer">The <see cref="BO.CustomerForList"/></param>
        /// <returns>A <see cref="BaseStationForList"/></returns>
        public static CustomerForList ConvertCustomer(BO.CustomerForList customer)
        {
            return new()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Mail = customer.Mail,
                ParcelsOnWay = customer.ParcelsOnWay,
                ParcelsRecieved = customer.ParcelsRecieved,
                ParcelsSendAndNotSupplied = customer.ParcelsSendAndNotSupplied,
                ParcelsSendAndSupplied = customer.ParcelsSendAndSupplied,
            };
        }

        /// <summary>
        /// return specific customer for list
        /// </summary>
        /// <param name="id">Id of requested customer</param>
        /// <returns>The <see cref="CustomerForList"/> who has the spesific Id</returns>
        public static CustomerForList GetCustomerForList(int id)
        {
            return ConvertCustomer(bl.GetCustomerForList(id));
        }

        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId">customer to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        public static void UpdateCustomer(int customerId, string name = null, string phone = null, string mail = null)
        {
            bl.UpdateCustomer(customerId, name, phone, mail);
            PLNotification.CustomerNotification.NotifyItemChanged(customerId);
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void DeleteCustomer(int customerId)
        {
            bl.DeleteCustomer(customerId);
            PLNotification.CustomerNotification.RemoveHandler(customerId);
            PLNotification.CustomerNotification.NotifyItemChanged(customerId);
        }
    }
}
