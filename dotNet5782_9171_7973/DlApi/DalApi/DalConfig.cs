using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    class DalConfig
    {
        internal static string DalType;
        internal static string Namespace;

        static DalConfig()
        {
            try
            {
                XElement dalConfig = XElement.Load("config.xml");
                string dalName = dalConfig.Element("dal").Value;
                var dalPackage = dalConfig.Element("dal-packages")
                                          .Element(dalName);

                DalType = dalPackage.Element("class-name").Value;
                Namespace = dalPackage.Element("namespace").Value;
            }
            catch (Exception e) 
            {
                throw new DalConfigException("Can't get data from config file", e);
            }

        }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string message) : base(message) { }
        public DalConfigException(string message, Exception inner) : base(message, inner) { }
    }
}
