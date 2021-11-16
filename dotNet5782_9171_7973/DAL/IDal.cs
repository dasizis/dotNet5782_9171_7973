using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    public interface IDal
    {
        (int, int, int, int, int) GetElectricityConfumctiol();
        void Add(DO.IIdentifiable item);
        IEnumerable GetList(Type type);
        DO.IIdentifiable GetById(Type type, int id);
        IEnumerable GetNotAssignedToDroneParcels();
        IEnumerable GetAvailableBaseStations();
        void AssignParcelToDrone(int parcelId, int droneId);
        void CollectParcel(int parcelId);
        void SupplyParcel(int parcelId);
        void ChargeDroneAtBaseStation(int droneId, int baseStationId);
        void FinishCharging(int droneId);
    }
}
