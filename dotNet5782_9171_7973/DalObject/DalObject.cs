using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DO;
using System.Linq;
using Singelton;

namespace Dal
{ 
    public sealed partial class DalObject : Singleton<DalObject>, DalApi.IDal
    {
        private DalObject() { }
        static  DalObject() { }

        #region Create
        /// <summary>
        /// Add an item to its data list
        /// </summary>
        /// <param name="item">the item to add</param>.
        public void Add<T>(T item) where T : IIdentifiable
        {
            Type type = typeof(T);
            if (DoesExist<T>(item.Id))
            {
                throw new IdAlreadyExistsException(type, item.Id);
            }

            DataSource.Data[type].Add(item);
        }

        #endregion

        #region Request
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
        public int GetParcelContNumber()
        {
            return DataSource.Config.NextParcelID++;
        }

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return DataSource.DroneCharges.Where(_ => true);
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
        #endregion

        #region Update
        public void Update<T>(int id, string propName, object newValue) where T : IIdentifiable
        {
            Type type = typeof(T);
            T item = DataSource.Data[type].Cast<T>().FirstOrDefault(item => item.Id == id)
                     ?? throw new ObjectNotFoundException(type, id);
            DataSource.Data[type].Remove(item);

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
            DataSource.Data[type].Add(item);
        }
        public void ChargeDrone(int droneId, int stationId)
        {
            if (!DoesExist<Drone>(droneId))
                throw new ObjectNotFoundException(typeof(Drone), droneId);
            if (!DoesExist<BaseStation>(stationId))
                throw new ObjectNotFoundException(typeof(BaseStation), stationId);

            DataSource.DroneCharges.Add(
                new DroneCharge()
                {
                    DroneId = droneId,
                    StationId = stationId,
                    StartTime = DateTime.Now,
                    IsDeleted = false,
                });
        }

        #endregion

        #region Delete
        public void FinishCharging(int droneId)
        {
            if (DoesExist<Drone>(droneId))
                throw new ObjectNotFoundException(typeof(Drone), droneId);
            // TO DO

            var charge = DataSource.DroneCharges.First(charge => charge.DroneId == droneId && !charge.IsDeleted);
            DataSource.DroneCharges.Remove(charge);
            charge.IsDeleted = true;
            DataSource.DroneCharges.Add(charge);
        }
        #endregion

        static private bool DoesExist<T>(int id) where T : IIdentifiable
        {
            return DataSource.Data[typeof(T)].Cast<IIdentifiable>().Any(item => item.Id == id);
        }
    }
}
