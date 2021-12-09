using System;
using System.Collections.Generic;
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
            string dalType = DalConfig.DalName;
            string dalPackage = DalConfig.DalPackages[dalType];

            if (dalPackage == null) throw new DalConfigException("There is no such package");

            try
            {
                Assembly.Load(dalPackage);
            }
            catch (System.IO.FileNotFoundException)
            {
                throw;
            }
            catch (System.IO.FileLoadException)
            {
                throw;
            }

            Type type = Type.GetType($"Dal.{dalPackage}, {dalPackage}");
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

