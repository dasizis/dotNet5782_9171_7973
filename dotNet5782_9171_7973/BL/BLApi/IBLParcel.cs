using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IBLParcel
    {
        void AddParcel(int senderId, int targetId, WeightCategory weight, Priority priority);
        void AssignParcelToDrone(int droneId);
        void PickUpParcel(int parcelId);
        IEnumerable<ParcelForList> GetNotAssignedToDroneParcels();
        void SupplyParcel(int droneId);
        IEnumerable<ParcelForList> GetParcelsList();
        Parcel GetParcel(int id);
    }
}
