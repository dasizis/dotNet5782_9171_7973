using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DO;
using System.Linq;
using DS;

using System.Reflection;
using DalApi;

namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Add an item to its data list
        /// </summary>
        /// <param name="item">the item to add</param>.
        public void Add<T>(T item) where T : IIdentifiable
        {
            Type type = typeof(T);
            if (DataSource.Data[type].Cast<IIdentifiable>().Any(obj => obj.Id == item.Id))
            {
                throw new IdAlreadyExistsException(type, item.Id);
            }

            DataSource.Data[type].Add(item);
        }

        public IEnumerable<T> GetFilteredList<T>(Func<T, bool> predicate)
        {
            return GetList<T>().Where(predicate);

        }

        /// <summary>
        /// return a copy of the wanted Data list
        /// </summary>
        /// <param name="type">the list type</param>
        /// <returns>a copy of the last</returns>
        public IEnumerable<T> GetList<T>()
        {
            return DataSource.Data[typeof(T)].Cast<T>();
        }

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

        public void Update<T>(int id, string propName, object newValue) where T : IIdentifiable
        {
            Type type = typeof(T);
            T item = DataSource.Data[type].Cast<T>().FirstOrDefault(item => item.Id == id)
                     ?? throw new ObjectNotFoundException(type, id);
            var prop = type.GetProperty(propName)
                     ?? throw new ArgumentException($"Type {type.Name} does not have property {propName}");

            try
            {
                prop.SetValue(item, newValue);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Can not set property {prop.Name} with value {newValue} of type {newValue.GetType().Name}", ex);
            }
        }

        public int GetParcelContNumber()
        {
            return DataSource.Config.NextParcelID++;
        }

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return DataSource.DroneCharges.Where(_ => true);
        }

        public void FinishCharging(int droneId)
        {
            throw new NotImplementedException();
        }
    }
}
