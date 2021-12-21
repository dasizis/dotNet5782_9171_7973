using System;
using System.Collections.Generic;

namespace DalApi
{
    /// <summary>
    /// The Dal layer interface
    /// </summary>
    public interface IDal
    {
        #region Create

        /// <summary>
        /// Adds an item to its data list
        /// </summary>
        /// <param name="item">the item to add</param>
        /// <exception cref="DO.IdAlreadyExistsException"></exception>
        void Add<T>(T item) where T : DO.IIdentifiable, DO.IDeletable;

        #endregion

        #region Request

        /// <summary>
        /// Finds a item of type T with the given Id
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="id">The item Id</param>
        /// <returns>The found item</returns>
        /// <exception cref="DO.ObjectNotFoundException"></exception>
        T GetById<T>(int id) where T : DO.IIdentifiable, DO.IDeletable;

        /// <summary>
        ///  Finds a item of type T that meets the conditon predicate
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="predicate">The condition</param>
        /// <returns>The found item</returns>
        /// <exception cref="DO.ObjectNotFoundException">
        /// If there is no such item or there is more than one item that qualifies
        /// </exception>
        T GetSingle<T>(Predicate<T> predicate) where T : DO.IDeletable;

        /// <summary>
        /// Returns all undeleted items of type T
        /// </summary>
        /// <typeparam name="T">The items type</typeparam>
        /// <returns>The items list</returns>
        IEnumerable<T> GetList<T>() where T : DO.IDeletable;

        /// <summary>
        ///  Returns all items of type T which meet the conditon predicate
        /// </summary>
        /// <typeparam name="T">The items type</typeparam>
        /// <param name="predicate">The condition</param>
        /// <returns>The filtered items list</returns>
        IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T : DO.IDeletable;

        /// <summary>
        /// Returns the parcel continuous number
        /// </summary>
        /// <returns>The parcel continuous number</returns>
        int GetParcelContinuousNumber();

        /// <summary>
        /// Returns a tuple of all the electricity confumctiol details
        /// </summary>
        /// <returns>A tuple of all the electricity confumctiol details</returns>
        (double, double, double, double, double) GetElectricityConfumctiol();

        #endregion

        #region Update

        /// <summary>
        /// Updates the item number id of type T
        /// Sets item.propName to newValue
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="id">The item id</param>
        /// <param name="propName">The property name</param>
        /// <param name="newValue">The new value for the property</param>
        /// <exception cref="DO.ObjectNotFoundException"></exception>
        /// <exception cref="ArgumentException">if the newValue type does not much the type of the property</exception>
        void Update<T>(int id, string propName, object newValue) where T : DO.IIdentifiable, DO.IDeletable;

        /// <summary>
        /// Updates all the items of type T which meet the condition predicate
        /// Sets item.propName to newValue
        /// </summary>
        /// <typeparam name="T">The items type</typeparam>
        /// <param name="predicate">The condition</param>
        /// <param name="propName">The property name</param>
        /// <param name="newValue">The new value for the property</param>
        /// <exception cref="ArgumentException">if the newValue type does not much the type of the property</exception>
        void UpdateWhere<T>(Predicate<T> predicate, string propName, object newValue) where T : DO.IDeletable;

        #endregion

        #region Delete

        /// <summary>
        /// Deletes item number id of type T
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="id">The item id</param>
        /// <exception cref="DO.ObjectNotFoundException"></exception>
        void Delete<T>(int id) where T : DO.IIdentifiable, DO.IDeletable;

        /// <summary>
        /// Deletes all the items of type T that meet the condition predicate number
        /// </summary>
        /// <typeparam name="T">The items type</typeparam>
        /// <param name="predicate">The condition</param>
        void DeleteWhere<T>(Predicate<T> predicate) where T : DO.IDeletable;
        #endregion
    }
}