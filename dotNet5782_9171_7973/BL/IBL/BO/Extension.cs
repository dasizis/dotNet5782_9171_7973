using System;
using System.Collections;

namespace IBL.BO
{
    public static class Extension
    {
        static string CamelCaseToReadable(string camelCase)
        {
            char[] letters = camelCase.ToCharArray();
            string result = "";

            foreach (var l in letters)
            {
                string letter = l.ToString();
                result += letter == letter.ToUpper() ? $" {letter}" : letter;
            }

            return letters[0] + result[2..];
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
}