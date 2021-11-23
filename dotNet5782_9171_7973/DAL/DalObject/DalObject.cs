using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using IDAL.DO;
using System.Linq;

using System.Reflection;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Add an item to its data list
        /// </summary>
        /// <param name="item">the item to add</param>.


        /// <summary>
        /// Remove an item from its data list
        /// </summary>
        /// <param name="item"></param>
        public void Remove<T>(int id) where T : IIdentifiable
        {
            int itemIndex = DataSource.data[typeof(T)].Cast<T>().ToList().FindIndex(item => item.Id == id);
            DataSource.data[typeof(T)].RemoveAt(itemIndex);    
        }

        static IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T : IIdentifiable =>
            DataSource.data[typeof(T)].Cast<T>().Where(item => predicate(item));

        /// <summary>
        /// return a copy of the wanted data list
        /// </summary>
        /// <param name="type">the list type</param>
        /// <returns>a copy of the last</returns>
        public IEnumerable<T> GetList<T>() where T : IIdentifiable => GetFilteredList<T>(_ => true);

        /// <summary>
        /// returns an item with the given type and id 
        /// </summary>
        /// <param name="type">the item's type</param>
        /// <param name="id">the item's id</param>
        /// <returns>the wanted item</returns>
        public T GetById<T>(int id) where T: IIdentifiable
        {
            try
            {
                return GetFilteredList<T>(item => item.Id == id).First();
            }
            catch (InvalidOperationException)
            {
                throw new ObjectNotFoundException(typeof(T), id);
            }
        }

        /// <summary>
        /// returns tuple of all the electricity confumctiol details
        /// </summary>
        /// <returns>tuple of all the electricity confumctiol details</returns>
        public (double, double, double, double, double) GetElectricityConfumctiol()
        {
            return
            (
                DataSource.Config.ElectricityConfumctiol.Free,
                DataSource.Config.ElectricityConfumctiol.Light,
                DataSource.Config.ElectricityConfumctiol.Medium,
                DataSource.Config.ElectricityConfumctiol.Heavy,
                DataSource.Config.ChargeRate
            );

        }

        public IEnumerable<Parcel> GetNotAssignedToDroneParcels()
        {
            return DataSource.parcels.Where(parcel => parcel.DroneId == 0);
        }

        public IEnumerable<BaseStation> GetAvailableBaseStations()
        {
            return DataSource.baseStations.FindAll(
                            baseStation => baseStation.ChargeSlots > DataSource.droneCharges.FindAll(
                                droneCharge => droneCharge.StationId == baseStation.Id).Count);
        }

        public void AssignParcelToDrone(int parcelId, int droneId)
        {
            Parcel parcel = GetById<Parcel>(parcelId);
            DataSource.parcels.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        public void SupplyParcel(int parcelId)
        {
            Parcel parcel = GetById<Parcel>(parcelId);
            DataSource.parcels.Remove(parcel);

            parcel.Supplied = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        public void ChargeDroneAtBaseStation(int droneId, int baseStationId)
        {
            DataSource.droneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = baseStationId });
        }

        public void FinishCharging(int droneId)
        {
            var droneCharge = DataSource.droneCharges.First(charge => charge.DroneId == droneId);
            DataSource.droneCharges.Remove(droneCharge);
        }

        public void CollectParcel(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T item) where T : IIdentifiable
        {
            Remove<T>(item.Id);
            Add(item);
        }

        public int GetParcelContNumber()
        {
            return DataSource.Config.NextParcelID;
        }

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return DataSource.droneCharges.Where(_ => true);
        }

        public void Add<T>(T item) where T : IIdentifiable
        {
            if (DataSource.data[typeof(T)].Cast<IIdentifiable>().Any(obj => obj.Id == item.Id))
            {
                throw new IdAlreadyExistsException(typeof(T), item.Id);
            }

            DataSource.data[typeof(T)].Add(item);
        }

        
    }
}
