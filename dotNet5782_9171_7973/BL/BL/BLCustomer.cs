
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BO;

namespace BL
{
    /// <summary>
    /// Implemens the <see cref="BLApi.IBLCustomer"/> part of the <see cref="BLApi.IBL"/>
    /// </summary>
    public partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            var customer = new Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Location = new Location() { Longitude = longitude, Latitude = latitude },
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerForList> GetCustomersList()
        {
            return from customer in dal.GetList<DO.Customer>()
                   select GetCustomerForList(customer.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            if (name != null)
            {
                if (!Validation.IsValidName(name))
                    throw new InvalidPropertyValueException(nameof(DO.Customer.Name), name);

                try
                {
                    dal.Update<DO.Customer>(customerId, nameof(DO.Customer.Name), name);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }

            if (phone != null)
            {
                if (!Validation.IsValidPhone(phone))
                {
                    throw new InvalidPropertyValueException(nameof(DO.Customer.Phone), phone);
                }

                try
                {
                    dal.Update<DO.Customer>(customerId, nameof(DO.Customer.Phone), phone);
                }
                catch (DO.ObjectNotFoundException e)
                {
                    throw new ObjectNotFoundException(typeof(BaseStation), e);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int customerId)
        {
            CustomerForList customer = GetCustomerForList(customerId);

            if (customer.ParcelsOnWay != 0 || customer.ParcelsSendAndNotSupplied != 0 || customer.ParcelsRecieved != 0 || customer.ParcelsSendAndSupplied != 0)
                throw new InvalidActionException("Can not delete a customer with parcels");

            dal.Delete<DO.Customer>(customerId);
        }

        #region Helpers

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

        #endregion
    }
}
