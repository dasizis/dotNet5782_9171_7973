using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DlApi
{
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        static DalConfig()
        {
            try
            {
                XElement dalConfig = XElement.Load("config.xml");
                DalName = dalConfig.Element("dal").Value;
                DalPackages = (from package in dalConfig.Element("dal-packages").Elements()
                               select package
                              ).ToDictionary(p => $"{p.Name}", p => p.Value);
            }
            catch (Exception e) { Console.WriteLine(e); }

        }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string message) : base(message) { }
        public DalConfigException(string message, Exception inner) : base(message, inner) { }
    }
}
