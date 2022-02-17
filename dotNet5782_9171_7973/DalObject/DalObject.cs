using DO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Dal
{
    /// <summary>
    /// Implements the <see cref="DalApi.IDal"/> interface using objects to store the data
    /// </summary>
    public sealed partial class DalObject : Singleton<DalObject>, DalApi.IDal
    {
        private DalObject() { }

        static  DalObject() { }

        #region Create

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add<T>(T item) where T : IIdentifiable, IDeletable
        {
            Type type = typeof(T);
            if (DoesExist<T>(obj => obj.Id == item.Id))
            {
                throw new IdAlreadyExistsException(type, item.Id);
            }

            item.IsDeleted = false;
            DataSource.Data[type].Add(item);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(int droneId, int baseStationId)
        {
            if (DoesExist<DroneCharge>(charge => charge.DroneId == droneId))
                throw new IdAlreadyExistsException(typeof(DroneCharge), droneId);

            if (!DoesExist<Drone>(d => d.Id == droneId))
                throw new ObjectNotFoundException(typeof(Drone));

            if (!DoesExist<BaseStation>(s => s.Id == baseStationId))
                throw new ObjectNotFoundException(typeof(BaseStation));
            

            DataSource.DroneCharges.Add(
                new DroneCharge()
                {
                    DroneId = droneId,
                    StationId = baseStationId,
                    StartTime = DateTime.Now,
                    IsDeleted = false,
                }
            );
        }

        #endregion

        #region Request

        [MethodImpl(MethodImplOptions.Synchronized)]
        public T GetById<T>(int id) where T : IIdentifiable, IDeletable
        {
            return GetSingle<T>(item => item.Id == id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public T GetSingle<T>(Predicate<T> predicate) where T : IDeletable
        {
            try
            {
                return GetFilteredList(predicate).Single();
            }
            catch (InvalidOperationException e)
            {
                throw new ObjectNotFoundException(typeof(T), e);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<T> GetList<T>() where T: IDeletable
        {
            return DataSource.Data[typeof(T)].Cast<T>().Where(item => !item.IsDeleted);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T: IDeletable
        {
            return GetList<T>().Where(item => predicate(item));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetParcelContinuousNumber()
        {
            return DataSource.Config.NextParcelId++;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
        #endregion

        #region Update

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update<T>(int id, string propName, object newValue) where T : IIdentifiable, IDeletable
        {
            T item = GetById<T>(id);
            UpdateItem(item, propName, newValue);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateWhere<T>(Predicate<T> predicate, string propName, object newValue) where T : IDeletable
        {
            for (int i = 0; i < DataSource.Data[typeof(T)].Count; i++)
            {
                T item = DataSource.Data[typeof(T)].Cast<T>().ElementAt(i);

                if (!item.IsDeleted && predicate(item))
                    UpdateItem(item, propName, newValue);
            }
        }

        #endregion

        #region Delete

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete<T>(int id) where T : IIdentifiable, IDeletable
        {
            Update<T>(id, nameof(IDeletable.IsDeleted), true);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteWhere<T>(Predicate<T> predicate) where T : IDeletable
        {
            UpdateWhere(predicate, nameof(IDeletable.IsDeleted), true);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks whether an item of type T with a given id is exists (and not deleted)
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="id">The item id</param>
        /// <returns>true if the item exists otherwise false</returns>
        private bool DoesExist<T>(Predicate<T> predicate) where T : IDeletable
        {
            return GetFilteredList(predicate).Any();
        }

        /// <summary>
        /// Update the given item
        /// setes item.propName to newValue
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="item">The item itself</param>
        /// <param name="propName">The property name</param>
        /// <param name="newValue">The new value for the property</param>
        private static void UpdateItem<T>(T item, string propName, object newValue)
        {
            Type type = typeof(T);
            DataSource.Data[type].Remove(item);
            PropertyInfo prop = type.GetProperty(propName)
                                ?? throw new ArgumentException($"Type {type.Name} does not have property {propName}");

            object boxed = item;
            try
            {
                prop.SetValue(boxed, newValue);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Can not set property {prop.Name} with value {newValue} of type {newValue.GetType().Name}", ex);
            }

            DataSource.Data[type].Add((T)boxed);
        }

        #endregion
    }
}
