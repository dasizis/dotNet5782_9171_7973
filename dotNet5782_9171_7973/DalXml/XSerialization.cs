using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    static class XSerialization
    {
        /// <summary>
        /// Converts an object to <see cref="XElement"/>
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="obj">The object</param>
        /// <returns>An <see cref="XElement"/> representation of the object</returns>
        internal static XElement ToXElement(this object obj)
        {
            using var memoryStream = new MemoryStream();
            using TextWriter streamWriter = new StreamWriter(memoryStream);

            var xmlSerializer = new XmlSerializer(obj.GetType());
            xmlSerializer.Serialize(streamWriter, obj);

            return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
        }

        /// <summary>
        /// Converts <see cref="XElement"/> to regular object   
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="xElement">The <see cref="XElement"/></param>
        /// <returns>The converted object</returns>
        internal static T FromXElement<T>(this XElement xElement)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }
    }
}
