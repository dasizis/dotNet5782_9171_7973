using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    public interface IDal
    {
        (double, double, double, double, double) GetElectricityConfumctiol();
        void Add<T>(T item) where T : DO.IIdentifiable;
        public void Remove<T>(T item) where T : DO.IIdentifiable;       
        IEnumerable<T> GetList<T>() where T : DO.IIdentifiable;
        IEnumerable<DO.DroneCharge> GetDroneCharges();
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
