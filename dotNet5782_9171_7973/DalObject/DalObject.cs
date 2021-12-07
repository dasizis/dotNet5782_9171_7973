using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DO;
using System.Linq;
using DS;

using System.Reflection;

namespace Dal
{
    public partial class DalObject : DlApi.IDL
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
            int itemIndex = DataSource.Data[typeof(T)].Cast<T>().ToList().FindIndex(item => item.Id == id);
            DataSource.Data[typeof(T)].RemoveAt(itemIndex);    
        }

    
        public IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T : IIdentifiable
        {
            return from item in DataSource.Data[typeof(T)].Cast<T>()
                   where predicate(item)
                   select item.Clone();

        }

        /// <summary>
        /// return a copy of the wanted Data list
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
            return DataSource.Parcels.Where(parcel => parcel.DroneId == null);
        }

        public IEnumerable<BaseStation> GetAvailableBaseStations()
        {
            return from station in DataSource.BaseStations
                   let dronesCount = (from charge in DataSource.DroneCharges 
                                      where charge.StationId == station.Id 
                                      select charge).Count()
                   where station.ChargeSlots > dronesCount
                   select station.Clone();
        }

        public void AssignParcelToDrone(int parcelId, int droneId)
        {
            Parcel parcel = GetById<Parcel>(parcelId);
            DataSource.Parcels.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            DataSource.Parcels.Add(parcel);
        }

        public void SupplyParcel(int parcelId)
        {
            Parcel parcel = GetById<Parcel>(parcelId);
            DataSource.Parcels.Remove(parcel);

            parcel.Supplied = DateTime.Now;
            DataSource.Parcels.Add(parcel);
        }

        public void ChargeDroneAtBaseStation(int droneId, int baseStationId)
        {
            DataSource.DroneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = baseStationId });
        }

        public void FinishCharging(int droneId)
        {
            var droneCharge = DataSource.DroneCharges.First(charge => charge.DroneId == droneId);
            DataSource.DroneCharges.Remove(droneCharge);
        }

        public void CollectParcel(int parcelId)
        {
            Parcel parcel = GetById<Parcel>(parcelId);
            DataSource.Parcels.Remove(parcel);

            parcel.PickedUp = DateTime.Now;
            DataSource.Parcels.Add(parcel);
        }

        public void Update<T>(T item) where T : IIdentifiable
        {
            Remove<T>(item.Id);
            Add(item);
        }

        public int GetParcelContNumber()
        {
            return DataSource.Config.NextParcelID++;
        }

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return DataSource.DroneCharges.Where(_ => true);
        }

        public void Add<T>(T item) where T : IIdentifiable
        {
            if (DataSource.Data[typeof(T)].Cast<IIdentifiable>().Any(obj => obj.Id == item.Id))
            {
                throw new IdAlreadyExistsException(typeof(T), item.Id);
            }

            DataSource.Data[typeof(T)].Add(item);
        }
    }
}
