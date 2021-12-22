using System;
using System.Collections.Generic;
using System.Linq;
using BO;

namespace BL
{
    partial class BL
    {
        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
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
                Id = dal.GetParcelContinuousNumber(),
                Priority = priority,
                Weight = weight,
                Sender = sender,
                Target = target,
                Requested = DateTime.Now,
            };

            try
            {
                dal.Add(new DO.Parcel()
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
        }

        public Parcel GetParcel(int id)
        {
            DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<DO.Parcel>(id);
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

        public IEnumerable<ParcelForList> GetParcelsList()
        {
            return from parcel in dal.GetList<DO.Parcel>()
                   select GetParcelForList(parcel.Id);
        }

        public IEnumerable<ParcelForList> GetNotAssignedToDroneParcels()
        {
            return from parcel in dal.GetFilteredList<DO.Parcel>(parcel => parcel.DroneId == null)
                   select GetParcelForList(parcel.Id); 
        }

        public void PickUpParcel(int droneId)
        {
            DroneForList drone = GetDroneForListRef(droneId);

            // Does the drone have a parcel?
            if (drone.DeliveredParcelId == null)
            {
                throw new InvalidActionException("No parcel is assigned to drone.");
            }

            DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<DO.Parcel>((int)drone.DeliveredParcelId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Parcel), e);
            }

            // Was the parcel collected?
            if (parcel.PickedUp != null)
            {
                throw new InvalidActionException("Parcel assigned to drone was already picked.");
            }

            ParcelInDeliver parcelInDeliver = GetParcelInDeliver(parcel.Id);
            dal.Update<DO.Parcel>(parcel.Id, nameof(parcel.PickedUp), DateTime.Now);

            drone.Battery -= Localable.Distance(drone.Location, parcelInDeliver.CollectLocation) * ElectricityConfumctiolFree * 0.1;
            drone.Location = parcelInDeliver.CollectLocation;
        }
       
        public void SupplyParcel(int droneId)
        {
            DroneForList drone = GetDroneForListRef(droneId);

            if (drone.DeliveredParcelId == null)
            {
                throw new InvalidActionException("No parcel is assigned to drone.");
            }

            var parcel = GetParcel((int)drone.DeliveredParcelId);

            if (parcel.PickedUp == null)
            {
                throw new InvalidActionException("Parcel assigned to drone was not picked up yet.");
            }

            ParcelInDeliver parcelInDeliver = GetParcelInDeliver(parcel.Id);

            if (!parcelInDeliver.WasPickedUp)
            {
                throw new InvalidActionException("Parcel assigned to drone has already been supplied.");
            }

            drone.Battery -= Localable.Distance(drone.Location, parcelInDeliver.TargetLocation) * GetElectricity(parcelInDeliver.Weight);
            drone.Location = parcelInDeliver.TargetLocation;
            drone.State = DroneState.Free;

            dal.Update<DO.Parcel>(parcel.Id, nameof(parcel.Supplied), DateTime.Now);
        }

        public void DeleteParcel(int parcelId)
        {
            try
            {
                dal.Delete<DO.Drone>(parcelId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Drone), e);
            }
        }

        #region Helpers

        /// <summary>
        /// Returns a converted parcel to parcel for list
        /// </summary>
        /// <param name="id">The parcel id</param>
        /// <returns>A <see cref="ParcelForList"/></returns>
        /// <exception cref="ObjectNotFoundException" />
        internal ParcelForList GetParcelForList(int id)
        {
            var parcel = GetParcel(id);

            return new ParcelForList()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                SenderName = parcel.Sender.Name,
                TargetName = parcel.Target.Name,
            };
        }

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
                parcel = dal.GetById<DO.Parcel>(id);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Parcel), e);
            }

            DO.Customer targetCustomer;
            try
            {
                targetCustomer = dal.GetById<DO.Customer>(parcel.SenderId);
            }
            catch (DO.ObjectNotFoundException e)
            {
                throw new ObjectNotFoundException(typeof(Customer), e);
            }

            DO.Customer senderCustomer;
            try
            {
                senderCustomer = dal.GetById<DO.Customer>(parcel.TargetId);
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
