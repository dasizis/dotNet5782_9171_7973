using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public static class Cloning
    {
        public static T Clone<T>(this T source)
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
