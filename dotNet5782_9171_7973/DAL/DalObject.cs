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
        public void Add(IIdentifiable item)
        {
            Type type = item.GetType();

            if (DataSource.data[type].Cast<IIdentifiable>().Any(obj => obj.Id == item.Id))
            {
                throw new IdAlreadyExistsException(type, item.Id);
            }

            DataSource.data[type].Add(item);
        }


        /// <summary>
        /// Remove an item from its data list
        /// </summary>
        /// <param name="item"></param>
        public void Remove(IIdentifiable item)
        {
            Type type = item.GetType();

            DataSource.data[type].Remove(item);    
        }


        /// <summary>
        /// returns a filtered list of the given type 
        /// </summary>
        /// <param name="type">the wanted list type</param>
        /// <param name="predicate">a predicate function</param>
        /// <returns>a filtered list </returns>
        IEnumerable getFilteredList(Type type, Predicate<IIdentifiable> predicate) =>
            DataSource.data[type].Cast<IIdentifiable>().Where(item => predicate(item));

        /// <summary>
        /// return a copy of the wanted data list
        /// </summary>
        /// <param name="type">the list type</param>
        /// <returns>a copy of the last</returns>
        public IEnumerable GetList(Type type) => getFilteredList(type, _ => true);

        /// <summary>
        /// returns an item with the given type and id 
        /// </summary>
        /// <param name="type">the item's type</param>
        /// <param name="id">the item's id</param>
        /// <returns>the wanted item</returns>
        public IIdentifiable GetById(Type type, int id)
        {
            try
            {
                return getFilteredList(type, item => item.Id == id).Cast<IIdentifiable>().First();
            }
            catch (InvalidOperationException)
            {
                throw new ObjectNotFoundException(type, id);
            }
        }

        /// <summary>
        /// returns tuple of all the electricity confumctiol details
        /// </summary>
        /// <returns>tuple of all the electricity confumctiol details</returns>
        public (int, int, int, int, int) GetElectricityConfumctiol()
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

        public IEnumerable GetNotAssignedToDroneParcels()
        {
            return DataSource.parcels.Where(parcel => parcel.DroneId == 0);
        }

        public IEnumerable GetAvailableBaseStations()
        {
            return DataSource.baseStations.FindAll(
                            baseStation => baseStation.ChargeSlots > DataSource.droneCharges.FindAll(
                                droneCharge => droneCharge.StationId == baseStation.Id).Count);
        }

        public void AssignParcelToDrone(int parcelId, int droneId)
        {
            Parcel parcel = (Parcel)GetById(typeof(Parcel), parcelId);
            DataSource.parcels.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        public void SupplyParcel(int parcelId)
        {
            Parcel parcel = (Parcel)GetById(typeof(Parcel), parcelId);
            DataSource.parcels.Remove(parcel);

            parcel.Delivered = DateTime.Now;
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

    }
}
