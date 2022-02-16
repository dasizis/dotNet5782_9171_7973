using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BO;

namespace BL
{
    partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            CustomerInDelivery sender;
            CustomerInDelivery target;

            try
            {
                sender = GetCustomerInDelivery(senderId);
                target = GetCustomerInDelivery(targetId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }

            var parcel = new Parcel()
            {
                Id = Dal.GetParcelContinuousNumber(),
                Priority = priority,
                Weight = weight,
                Sender = sender,
                Target = target,
                Requested = DateTime.Now,
            };

            lock (Dal) try
            {
                Dal.Add(new DO.Parcel()
                {
                    Id = parcel.Id,
                    SenderId = parcel.Sender.Id,
                    TargetId = parcel.Target.Id,
                    Priority = (DO.Priority)parcel.Priority,
                    Weight = (DO.WeightCategory)parcel.Weight,
                    DroneId = null,
                    Requested = parcel.Requested,
                });
            }
            catch (DO.IdAlreadyExistsException)
            {
                throw new IdAlreadyExistsException(typeof(Parcel), parcel.Id);
            }

            return parcel.Id;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            DO.Parcel parcel;
            try
            {
                parcel = Dal.GetById<DO.Parcel>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Parcel), e);
            }

            return new Parcel()
            {
                Id = parcel.Id,
                Drone = parcel.DroneId.HasValue ? GetDrone(parcel.DroneId.Value) : null,
                Sender = GetCustomerInDelivery(parcel.SenderId),
                Target = GetCustomerInDelivery(parcel.TargetId),
                Weight = (WeightCategory)parcel.Weight,
                Priority = (Priority)parcel.Priority,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Supplied = parcel.Supplied,
            };
        }

        /// <summary>
        /// Returns a converted parcel to parcel for list
        /// </summary>
        /// <param name="id">The parcel id</param>
        /// <returns>A <see cref="ParcelForList"/></returns>
        /// <exception cref="ObjectNotFoundException" />
        public ParcelForList GetParcelForList(int id)
        {
            Parcel parcel = GetParcel(id);

            return new ParcelForList()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Target.Id,
                IsOnWay = parcel.Scheduled != null && parcel.Supplied == null,
            };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelForList> GetParcelsList()
        {
            return from parcel in Dal.GetList<DO.Parcel>()
                   select GetParcelForList(parcel.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelForList> GetNotAssignedToDroneParcels()
        {
            return from parcel in Dal.GetFilteredList<DO.Parcel>(parcel => parcel.DroneId == null)
                   select GetParcelForList(parcel.Id); 
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int parcelId)
        {
            DO.Parcel parcel;
            try
            {
                parcel = Dal.GetById<DO.Parcel>(parcelId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Parcel), e);
            }

            if (parcel.Scheduled != null && parcel.Supplied == null)
                throw new InvalidActionException("Can not delete a parcel in delivery");

            lock (Dal)
            {
                Dal.Delete<DO.Parcel>(parcelId);
            }
        }

        #region Helpers

        /// <summary>
        /// return converted parcel to parcel at customer
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel at customer</returns>
        /// <exception cref="ObjectNotFoundException" />
        internal ParcelAtCustomer GetParcelAtCustomer(int id)
        {
            var parcel = GetParcel(id);

            var state = parcel.Requested == null ? ParcelState.Scheduled
                        : parcel.Scheduled == null ? ParcelState.Requested
                        : parcel.PickedUp == null ? ParcelState.PickedUp
                        : ParcelState.Supplied;

            return new ParcelAtCustomer()
            {
                Id = id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                OtherCustomer = parcel.Supplied != null ? parcel.Sender : parcel.Target,
                State = state,
            };
        }

        /// <summary>
        /// Returns a converted parcel to parcel in delivery
        /// </summary>
        /// <param name="id">The parcel Id</param>
        /// <returns>A <see cref="ParcelInDeliver"/></returns>
        /// <exception cref="ObjectNotFoundException" />
        internal ParcelInDeliver GetParcelInDeliver(int id)
        {
            DO.Parcel parcel;
            try
            {
                parcel = Dal.GetById<DO.Parcel>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Parcel), e);
            }

            DO.Customer targetCustomer;
            try
            {
                targetCustomer = Dal.GetById<DO.Customer>(parcel.TargetId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }

            DO.Customer senderCustomer;
            try
            {
                senderCustomer = Dal.GetById<DO.Customer>(parcel.SenderId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }

            var targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };
            var senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };

            return new ParcelInDeliver()
            {
                Id = id,
                Weight = (WeightCategory)parcel.Weight,
                Priority = (Priority)parcel.Priority,
                TargetLocation = targetLocation,
                CollectLocation = senderLocation,
                WasPickedUp = parcel.PickedUp != null,
                DeliveryDistance = Localable.Distance(senderLocation, targetLocation),
                Sender = GetCustomerInDelivery(senderCustomer.Id),
                Target = GetCustomerInDelivery(targetCustomer.Id),
            };
        }

        #endregion 

    }
}
