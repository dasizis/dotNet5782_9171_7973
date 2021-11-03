using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    static class Extension
    {
        internal static object GetById<T>(this List<T> list, int id) where T: IIdentifiable
        {
            try
            {
                return list.First(item => item.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ObjectNotFoundException(typeof(T), id);
            }
        }
        internal static bool DoesIdExist<T>(this List<T> list, int id) where T : IIdentifiable
        {
            return list.FindIndex(item => item.Id == id) != -1;
        }
    }
}
