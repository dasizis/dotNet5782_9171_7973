using System;

namespace Singleton
{
    /// <summary>
    /// An exception that occurs in <see cref="Singleton"/> class
    /// </summary>
    [Serializable]
    public class SingletonException : Exception
    {
        public SingletonException(string message) : base(message) { }
        public SingletonException(string message, Exception exception) : base(message, exception) { }
    }
}
