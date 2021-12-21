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

                dal.Update<DO.Customer>(customerId, nameProperty, name);
            }

            if (phone != null)
            {
                if (!Validation.IsValidPhone(phone))
                    throw new InvalidPropertyValueException(phoneProperty, phone);

                dal.Update<DO.BaseStation>(customerId, phoneProperty, phone);
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
    }
}
