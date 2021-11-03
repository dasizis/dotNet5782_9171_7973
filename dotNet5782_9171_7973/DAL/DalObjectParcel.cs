using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// adds a parcel to pacels list
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority)
        {
            Parcel parcel = new Parcel
            {
                Id = DataSource.Config.NextParcelID++,
                SenderId = senderId,
                TargetId = targetId,
                Weight = weight,
                Priority = priority,
                Requested = DateTime.Now,
                DroneId = 0
            };

            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// collects parcel by its drone
        /// </summary>
        /// <param name="parcelId"></param>
        public void CollectParcel(int parcelId)
        {
            Parcel parcel = (Parcel)DataSource.parcels.GetById(parcelId);

            DataSource.parcels.Remove(parcel);
            parcel.PickedUp = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// returns the parcels list
        /// </summary>
        /// <returns>an list of all the exist parcels</returns>
        public IEnumerable<Parcel> GetParcelList()
        {
            return DataSource.parcels;
        }

        /// <summary>
        /// finds all parcels not assigned to drones yet
        /// </summary>
        /// <returns>list of found parcels</returns>
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone()
        {
            return DataSource.parcels.FindAll(p => p.DroneId == 0);
        }
    }
}
