using BO;
using System.Collections.Generic;

namespace BLApi
{
    public interface IBLParcel
    {
        void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority);
        void PickUpParcel(int parcelId);
        IEnumerable<ParcelForList> GetNotAssignedToDroneParcels();
        void SupplyParcel(int droneId);
        IEnumerable<ParcelForList> GetParcelsList();
        Parcel GetParcel(int id);
    }
}
