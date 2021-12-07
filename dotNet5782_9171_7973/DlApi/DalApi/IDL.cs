using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DlApi
{
    public interface IDL
    {
        (double, double, double, double, double) GetElectricityConfumctiol();
        void Add<T>(T item) where T : DO.IIdentifiable;
        public void Remove<T>(int id) where T : DO.IIdentifiable;       
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
        void Update<T>(T item) where T : DO.IIdentifiable;
        int GetParcelContNumber();
        IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T : DO.IIdentifiable;  
    }
}
