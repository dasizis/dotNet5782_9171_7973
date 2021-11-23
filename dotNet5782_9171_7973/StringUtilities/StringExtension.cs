using System;
using System.Collections;

namespace StringUtilities
{
    public static class StringUtilitiesExtension
    {
        public static string ToStringProps<T>(this T obj)
        {
            Type type = obj.GetType();
            string description = "----------------\n" +
                                $"{type.Name}\n" +
                                 "----------------";

            foreach (var prop in type.GetProperties())
            {
                description += $"{Environment.NewLine}{prop.Name} = ";

                if (Attribute.IsDefined(prop, typeof(SexadecimalLatitudeAttribute)))
                {
                    description += Sexadecimal.Latitude((double)prop.GetValue(obj));
                }
                else if (Attribute.IsDefined(prop, typeof(SexadecimalLongitudeAttribute)))
                {
                    description += Sexadecimal.Longitde((double)prop.GetValue(obj));
                }
                else
                {
                    if (obj.GetType().GetMethod("ToString").DeclaringType != obj.GetType())
                    {
                        description += "\n";
                    }
}
                    description += prop.GetValue(obj).ToString();
                }
            }

            return description;
        }
    }
}
