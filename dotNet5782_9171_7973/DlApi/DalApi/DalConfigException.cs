using System;

namespace DalApi
{
    /// <summary>
    /// 
    /// </summary>
    public class DalConfigException : Exception
    {
        public DalConfigException(string message) : base(message) { }
        public DalConfigException(string message, Exception inner) : base(message, inner) { }
    }
}
