using System;
using System.Collections;

namespace StringUtilities
{
    public static class StringUtilitiesExtension
    {
        public static string ToStringProps<T>(this T obj, bool detailedNested = false, int nestLevel = 0)
        {
            Type type = obj.GetType();
            string description = "=============================" +
                                $"{type.Name}" +
                                 "=============================";

            foreach (var prop in type.GetProperties())
            {
                description += $"{Environment.NewLine}{prop.Name} = ";


                // Is the prop a list?
                if (prop.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    if (detailedNested)
                    {

                    }
                    else description += $"List<{prop.GetType().GetGenericArguments()}>{}"
                }
                else description += prop.GetValue(obj).ToString();
            }

            return description;
        }
    }
}
