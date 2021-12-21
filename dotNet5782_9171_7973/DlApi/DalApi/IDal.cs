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

        void Add<T>(T item) where T : DO.IIdentifiable, DO.IDeletable;

        #endregion

        #region Request

        T GetById<T>(int id) where T : DO.IIdentifiable, DO.IDeletable;

        T GetItem<T>(Predicate<T> predicate) where T : DO.IDeletable;

        IEnumerable<T> GetList<T>() where T : DO.IDeletable;

        IEnumerable<T> GetFilteredList<T>(Predicate<T> predicate) where T : DO.IDeletable;

        int GetParcelContNumber();

        (double, double, double, double, double) GetElectricityConfumctiol();

        #endregion

        #region Update

        void Update<T>(int id, string attribute, object newValue) where T : DO.IIdentifiable;

        #endregion

        #region Delete
        void Delete<T>(int id) where T : DO.IIdentifiable, DO.IDeletable;

        void DeleteWhere<T>(Predicate<T> predicate) where T : DO.IDeletable;
        #endregion
    }
}