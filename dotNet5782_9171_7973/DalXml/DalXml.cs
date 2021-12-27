//using System;
//using System.Collections.Generic;
//using DO;
//using System.Linq;
//using Singleton;
//using System.Reflection;
//using System.IO;
//using System.Xml.Linq;
//using System.Xml.Serialization;
//using System.Text;

//namespace Dal
//{ 

//    /// <summary>
//    /// Implements the <see cref="DalApi.IDal"/> interface using objects to store the data
//    /// </summary>
//    public sealed partial class DalXml : Singleton<DalXml>, DalApi.IDal
//    {
//        private DalXml() { }

//        static  DalXml() { }

//        private string XmlFilesLocation => $@"{Directory.GetCurrentDirectory()}\..\..\..\..\XmlData";

//        private string GetXmlFilePath(Type type) => $@"{XmlFilesLocation}\${type.Name}.xml";

//        private string ConfigFilePath => $@"{XmlFilesLocation}\config.xml";

//        #region Create

//    }
//}
