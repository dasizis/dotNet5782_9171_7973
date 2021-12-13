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
        /// add a parcel
        /// </summary>
        /// <param name="senderId">the parcel sender customer id</param>
        /// <param name="targetId">the parcel target customer id</param>
        /// <param name="weight">the parcel weight</param>
        /// <param name="priority">the parcel priority</param>
        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            var parcel = new Parcel()
            {
                Id = dal.GetParcelContNumber(),
                Priority = priority,
                Weight = weight,
                Sender = GetCustomerInDelivery(senderId),
                Target = GetCustomerInDelivery(targetId),
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
            catch
            {
                throw new IdAlreadyExistsException(typeof(Parcel), parcel.Id);
            }
        }
        /// <summary>
        /// return list of parcels which weren't assigned to drone yet
        /// </summary>
        /// <returns>list of parcels which weren't assigned to drone yet</returns>
        public IEnumerable<ParcelForList> GetNotAssignedToDroneParcels()
        {
            return from parcel in dal.GetFilteredList<DO.Parcel>(parcel => parcel.DroneId == null)
                   select GetParcelForList(parcel.Id); 
        }
        /// <summary>
        /// return parcels list
        /// </summary>
        /// <returns>parcels list</returns>
        public IEnumerable<ParcelForList> GetParcelsList()
        {
            return from parcel in dal.GetList<DO.Parcel>()
                   select GetParcelForList(parcel.Id);
        }
        /// <summary>
        /// drone picks up his assigned parcel
        /// </summary>
        /// <param name="droneId">drone picks</param>
        public void PickUpParcel(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);

            // Does the drone have a parcel?
            if (!drone.DeliveredParcelId.HasValue)
            {
                throw new InValidActionException("No parcel is assigned to drone.");
            }

            DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<DO.Parcel>(drone.DeliveredParcelId.Value);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Parcel), drone.DeliveredParcelId.Value);
            }

            // Was the parcel collected?
            if (parcel.PickedUp != null)
            {
                throw new InValidActionException("Parcel assigned to drone was already picked.");
            }

            ParcelInDeliver parcelInDeliver = GetParcelInDeliver(parcel.Id);

            try
            {
                dal.Update<DO.Parcel>(parcel.Id, nameof(parcel.PickedUp), DateTime.Now);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Parcel), parcel.Id);
            }

            drone.Battery -= Location.Distance(drone.Location, parcelInDeliver.CollectLocation) * ElectricityConfumctiolFree;
            drone.Location = parcelInDeliver.CollectLocation;


        }
        /// <summary>
        /// drone supplies its parcel at target
        /// </summary>
        /// <param name="droneId">drone which supplies</param>
        public void SupplyParcel(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);

            if (drone.DeliveredParcelId == null)
            {
                throw new InValidActionException("No parcel is assigned to drone.");
            }

            var parcel = GetParcel((int)drone.DeliveredParcelId);

            if (parcel.PickedUp == null)
            {
                throw new InValidActionException("Parcel assigned to drone was not picked up yet.");
            }

            ParcelInDeliver parcelInDeliver = GetParcelInDeliver(parcel.Id);

            if (parcelInDeliver.Position)
            {
                throw new InValidActionException("Parcel assigned to drone has already been supplied.");
            }

            drone.Battery -= Location.Distance(drone.Location, parcelInDeliver.TargetLocation) * GetElectricity(parcelInDeliver.Weight);
            drone.Location = parcelInDeliver.TargetLocation;
            drone.State = DroneState.Free;

            dal.Update<DO.Parcel>(parcel.Id, nameof(parcel.Supplied), DateTime.Now);
        }
       
    }
}
