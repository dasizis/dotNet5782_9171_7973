using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    static class Cloning
    {
        internal static T Clone<T>(this T source)
        {
            T target = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties())
            {
                prop.SetValue(target, prop.GetValue(source));
            }

            return target;
        }
    }
}
