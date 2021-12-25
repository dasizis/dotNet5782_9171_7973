using System;
using System.IO;
using System.Reflection;

namespace DalApi
{
    /// <summary>
    /// Dal layer factory
    /// </summary>
    public static class DalFactory
    {
        /// <summary>
        /// Dynamically loads the Dal assemly according to the <see cref="DalConfig"/> class 
        /// and access the dal instance using reflection
        /// </summary>
        /// <returns>The Dal instance</returns>
        public static IDal GetDal()                                                                                                          
        {
            Assembly.LoadFrom($@"{Directory.GetCurrentDirectory()}\..\..\..\..\{DalConfig.ClassName}\bin\Debug\net5.0\{DalConfig.ClassName}.dll");
            Type type = Type.GetType($"{DalConfig.Namespace}.{DalConfig.ClassName}, {DalConfig.ClassName}");

            if (type == null)
                throw new DalConfigException("Can't find such project");

            IDal dal = (IDal)type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                 .GetValue(null);

            if (dal == null)
                throw new DalConfigException("Can't Get Dal Instance");

            return dal;
        }
    }
}

