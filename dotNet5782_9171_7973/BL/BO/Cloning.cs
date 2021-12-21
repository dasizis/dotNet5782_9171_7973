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
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="source">The object to clone</param>
        /// <returns>A deep clone of the object</returns>
        internal static T Clone<T>(this T source)
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
            object target = Activator.CreateInstance(source.GetType());

            foreach (var prop in source.GetType().GetProperties())
            {
                var value = prop.GetValue(source);
                if (!value.GetType().IsValueType)
                {
                    value = value.CloneObject();
                }

                prop.SetValue(target, value);
            }

            return target;
        }
    }
}
