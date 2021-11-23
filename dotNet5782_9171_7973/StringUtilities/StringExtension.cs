using System;

namespace StringUtilities
{
    public static class StringUtilitiesExtension
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

    
        public static string ToStringProps<T>(this T obj)
        {
            Type type = obj.GetType();
            string description = $"{type.Name}";

            foreach (var prop in type.GetProperties())
            {
                // Is the prop a list?
                //if (prop.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                //{

                //}
                description += $"{Environment.NewLine}{prop.Name} = {prop.GetValue(obj)}";
            }

            return description;
        }
}
