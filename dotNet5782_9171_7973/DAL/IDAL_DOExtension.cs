using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public static class Extension
    {
        public static string ToStringProps<T>(this T obj)
        {
            Type type = obj.GetType();
            string description = $"{type.Name}";

            foreach (var prop in type.GetProperties())
            {
                description += $"{Environment.NewLine}{prop.Name} = {prop.GetValue(obj)}";
            }

            return description;
        }

    }
}
