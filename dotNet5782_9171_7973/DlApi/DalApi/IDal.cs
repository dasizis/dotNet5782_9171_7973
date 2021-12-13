using System;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        (int, int, int, int, int) GetElectricityConfumctiol();
        void Add<T>(T item) where T : DO.IIdentifiable;
        IEnumerable<T> GetList<T>();
        T GetById<T>(int id) where T : DO.IIdentifiable;
        void FinishCharging(int droneId);
        void Update<T>(int id, string attribute, object newValue) where T : DO.IIdentifiable;
        int GetParcelContNumber();
        IEnumerable<T> GetFilteredList<T>(Func<T, bool> predicate);
    }
}