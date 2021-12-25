using System;
using System.IO;
using System.Xml.Linq;

namespace DalApi
{
    /// <summary>
    /// Loads from a config.xml file the IDal implementation namespace and class-name
    /// then saves them in its own properites
    /// </summary>
    static class DalConfig
    {
        /// <summary>
        /// IDal implementation namespace
        /// </summary>
        internal static string Namespace;

        /// <summary>
        /// IDal implementation class-name
        /// </summary>
        internal static string ClassName;

        /// <summary>
        /// Loads the information from the config.xml file
        /// </summary>
        /// <exception cref="DalConfigException"></exception>
        static DalConfig()
        {
            XElement dalConfig;
            try
            {
                dalConfig = XElement.Load($@"{Directory.GetCurrentDirectory()}\..\..\..\..\config.xml");
            }
            catch (FileNotFoundException e)
            {
                throw new DalConfigException("Could not load config.xml file", e);
            }

            string dalName = dalConfig.Element("dal").Value;
            var dalPackage = dalConfig.Element("dal-packages")
                                      .Element(dalName);

            ClassName = dalPackage.Element("class-name").Value;
            Namespace = dalPackage.Element("namespace").Value;
        }
    }
}
