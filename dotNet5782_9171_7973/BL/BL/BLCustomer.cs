using System.Collections.Generic;
using System.Linq;
using BO;

namespace BL
{
    /// <summary>
    /// Implemens the <see cref="BLApi.IBLCustomer"/> part of the <see cref="BLApi.IBL"/>
    /// </summary>
    partial class BL
    {
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
            catch (DO.IdAlreadyExistsException)
            {
                throw new IdAlreadyExistsException(typeof(Customer), id);
            }
        }

        public Customer GetCustomer(int id)
        {
            DO.Customer customer;
            try
            {
                customer = dal.GetById<DO.Customer>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }

            var sendParcels = dal.GetFilteredList<DO.Parcel>(parcel => parcel.SenderId == id)
                                 .Select(parcel => GetParcel(parcel.Id));

            var targetParcels = dal.GetFilteredList<DO.Parcel>(parcel => parcel.TargetId == id)
                                   .Select(parcel => GetParcel(parcel.Id));


            return new Customer()
            {
                Id = customer.Id,
                Location = new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude },
                Name = customer.Name,
                Phone = customer.Phone,
                Send = sendParcels.ToList(),
                Recieve = targetParcels.ToList(),
            };
        }

        public IEnumerable<CustomerForList> GetCustomersList()
        {
            return from customer in dal.GetList<DO.Customer>()
                   select GetCustomerForList(customer.Id);
        }
        
        public void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            const string nameProperty = "Name";
            const string phoneProperty = "Phone";

            if (name != null)
            {
                if (!Validation.IsValidName(name))
                    throw new InvalidPropertyValueException(nameProperty, name);

                try
                {
                    dal.Update<DO.Customer>(customerId, nameProperty, name);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }

            if (phone != null)
            {
                if (!Validation.IsValidPhone(phone))
                    throw new InvalidPropertyValueException(phoneProperty, phone);

                try
                {
                    dal.Update<DO.Customer>(customerId, phoneProperty, phone);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }
        }

        public void DeleteCustomer(int customerId)
        {
            try
            {
                dal.Delete<DO.Customer>(customerId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }
        }

        /// <summary>
        /// Returns specific customer for list
        /// </summary>
        /// <param name="id">id of requested customer</param>
        /// <returns>customer with id</returns>
        /// <exception cref="ObjectNotFoundException" />
        internal CustomerForList GetCustomerForList(int id)
        {
            DO.Customer customer;

            try
            {
                customer = dal.GetById<DO.Customer>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }

            var parcels = dal.GetList<DO.Parcel>();

            return new CustomerForList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                ParcelsSendAndSupplied = dal.GetFilteredList<DO.Parcel>(p => p.SenderId == id && p.Supplied != null).Count(),
                ParcelsSendAndNotSupplied = dal.GetFilteredList<DO.Parcel>(p => p.SenderId == id && p.Supplied == null).Count(),
                ParcelsRecieved = dal.GetFilteredList<DO.Parcel>(p => p.TargetId == id && p.Supplied != null).Count(),
                ParcelsOnWay = dal.GetFilteredList<DO.Parcel>(p => p.TargetId == id && p.Supplied == null).Count(),
            };
        }

        /// <summary>
        /// return converted customer to customer in delivery
        /// </summary>
        /// <param name="id">id of requested customer</param>
        /// <returns>customer in delivery</returns>
        /// <exception cref="ObjectNotFoundException" />
        internal CustomerInDelivery GetCustomerInDelivery(int id)
        {
            DO.Customer customer;

            try
            {
                customer = dal.GetById<DO.Customer>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }

            return new CustomerInDelivery()
            {
                Id = id,
                Name = customer.Name,
            };
        }
    }
}
