using System;
using System.Collections.Generic;
using DO;
using System.Linq;
using Singleton;
using System.Reflection;

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

        public void Add<T>(T item) where T : IIdentifiable, IDeletable
        {
            Type type = typeof(T);
            if (DoesExist<T>(item.Id))
            {
                throw new IdAlreadyExistsException(type, item.Id);
            }

            Update<T>(item.Id, nameof(item.IsDeleted), false);
            DataSource.Data[type].Add(item);
        }

        #endregion

        #region Request

        public T GetById<T>(int id) where T : IIdentifiable, IDeletable
        {
            return GetSingle<T>(item => item.Id == id);
        }

        public T GetSingle<T>(Predicate<T> predicate) where T : IDeletable
        {
            try
            {
                return DataSource.Data[typeof(T)].Cast<T>().Single(i => predicate(i) && !i.IsDeleted);
            }
            catch (InvalidOperationException e)
            {
                throw new ObjectNotFoundException(typeof(T), e);
            }
        }

        public IEnumerable<T> GetList<T>() where T: IDeletable
        {
            return DataSource.Data[typeof(T)].Cast<T>().Where(item => !item.IsDeleted);
        }

        public IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T: IDeletable
        {
            return GetList<T>().Where(item => predicate(item));
        }

        public int GetParcelContinuousNumber()
        {
            return DataSource.Config.NextParcelID++;
        }

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

        public void Update<T>(int id, string propName, object newValue) where T : IIdentifiable, IDeletable
        {
            Type type = typeof(T);
            T item = GetById<T>(id) ?? throw new ObjectNotFoundException(type, null);

            UpdateItem(item, propName, newValue);
        }

        public void UpdateWhere<T>(Predicate<T> predicate, string propName, object newValue) where T : IDeletable
        {
            foreach (T item in GetFilteredList(predicate))
            {
                UpdateItem(item, propName, newValue);
            }
        }

        #endregion

        #region Delete

        public void Delete<T>(int id) where T : IIdentifiable, IDeletable
        {
            Update<T>(id, "IsDeleted", true);
        }

        public void DeleteWhere<T>(Predicate<T> predicate) where T : IDeletable
        {
            UpdateWhere(predicate, "IsDeleted", true);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks whether an item of type T with a given id is exists (and not deleted)
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="id">The item id</param>
        /// <returns>true if the item exists otherwise false</returns>
        static private bool DoesExist<T>(int id) where T : IIdentifiable, IDeletable
        {
            return DataSource.Data[typeof(T)].Cast<T>().Any(item => item.Id == id && !item.IsDeleted);
        }

        /// <summary>
        /// Update the given item
        /// setes item.propName to newValue
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="item">The item itself</param>
        /// <param name="propName">The property name</param>
        /// <param name="newValue">The new value for the property</param>
        static private void UpdateItem<T>(T item, string propName, object newValue)
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
