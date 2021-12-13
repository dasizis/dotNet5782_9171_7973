using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    public class FactoryDL
    {
        public IDal GetDL()                                                                                                          
        {
            //Assembly.LoadFile($@"{Directory.GetCurrentDirectory()}\{DalConfig.DalType}.dll");            
            Assembly.Load(DalConfig.DalType);
            Type type = Type.GetType($"{DalConfig.Namespace}.{DalConfig.DalType}, {DalConfig.DalType}");

            if (type == null)
            {
                throw new DalConfigException("Can't find such project");
            }

            IDal dal = (IDal)type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            

            if (dal == null)
            {
                throw new DalConfigException("Can't Get Dal Instance");
            }

            return dal;
        }
    }
}

