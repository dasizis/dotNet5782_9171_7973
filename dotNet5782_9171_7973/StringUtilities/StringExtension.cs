using System;
using System.Reflection;
using System.Text;

namespace StringUtilities
{
    public static class StringUtilitiesExtension
    {
        /// <summary>
        /// Converts an object to detailed string description
        /// using reflection
        /// </summary>
        /// <param name="obj">The object to describe</param>
        /// <returns>A detailed <see cref="string"/> description of the object</returns>
        public static string ToStringProperties(this object obj)
        {
            Type type = obj.GetType();
            StringBuilder description = new($"<{type.Name}> {{");

            foreach (var prop in type.GetProperties(BindingFlags.Public |
                                                    BindingFlags.Instance |
                                                    BindingFlags.DeclaredOnly))
            {
                description.Append($"{prop.Name} = ");

                var propValue = prop.GetValue(obj);
                var countProperty = prop.PropertyType.GetProperty("Count");

                // Is the property a list?
                if (countProperty != null)
                {
                    var listCount = countProperty.GetValue(propValue);
                    var listType = propValue.GetType().GetGenericArguments()[0].Name;

                    description.Append($"<List[{listType}](Count = {listCount})");
                }
                else if (Attribute.IsDefined(prop, typeof(SexadecimalLatitudeAttribute)))
                {
                    description.Append(Sexadecimal.Latitude((double)propValue));
                }
                else if (Attribute.IsDefined(prop, typeof(SexadecimalLongitudeAttribute)))
                {
                    description.Append(Sexadecimal.Longitde((double)propValue));
                }
                else
                {
                    description.Append(propValue?.ToString() ?? "null");
                }
                description.Append(", ");
            }

            string result = description.ToString();

            // Remove the last comma
            return result[..result.LastIndexOf(',')] + '}';
        }
    }
}
