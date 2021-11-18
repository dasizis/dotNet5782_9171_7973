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
        public void Remove<T>(int id) where T : DO.IIdentifiable;       
        IEnumerable<T> GetList<T>() where T : DO.IIdentifiable;
        T GetById<T>(int id) where T : DO.IIdentifiable;
        IEnumerable<DO.Parcel> GetNotAssignedToDroneParcels();
        IEnumerable<DO.BaseStation> GetAvailableBaseStations();
        void AssignParcelToDrone(int parcelId, int droneId);
        void CollectParcel(int parcelId);
        void SupplyParcel(int parcelId);
        void ChargeDroneAtBaseStation(int droneId, int baseStationId);
        void FinishCharging(int droneId);
        void Update<T>(DO.IIdentifiable item) where T : DO.IIdentifiable;
        int GetParcelContNumber();
  
    }
}
