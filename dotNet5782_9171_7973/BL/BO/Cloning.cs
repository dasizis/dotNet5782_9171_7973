using System;

namespace BO
{
    /// <summary>
    /// Static class for <see cref="Clone{T}(T)"/> extension method
    /// </summary>
    internal static class Cloning
    {

        /// <summary>
        /// Makes a deep clone of an object
        /// (But not of lists)
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="source">The object to clone</param>
        /// <returns>A deep clone of the object</returns>
        internal static T Clone<T>(this T source) where T : new()
        {
            return (T)source.CloneObject();
        }

        /// <summary>
        /// An helper method to make a deep clone of an object
        /// </summary>
        /// <param name="source">The object to clone</param>
        /// <returns>A deep clone of the object</returns>
        private static object CloneObject(this object source)
        {
            Type type = source.GetType();
            object target = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                var value = prop.GetValue(source);
                if (value != null && !value.GetType().IsValueType && value.GetType() != typeof(string))
                {
                    value = value.CloneObject();
                }

                prop.SetValue(target, value);
            }

            return target;
        }
    }
}
