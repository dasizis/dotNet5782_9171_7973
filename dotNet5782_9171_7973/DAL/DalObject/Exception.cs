using System;
using System.Runtime.Serialization;

namespace DalObject
{

    [Serializable]
    abstract public class IdException : Exception
    {
        public IdException() : base() { }
        public IdException(string message) : base(message) { }
        public IdException(string message, Exception inner) : base(message, inner) { }
        protected IdException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public Type Type { get; }
        public int Id { get; }

        public IdException(Type type, int id) : base()
        {
            Type = type;
            Id = id;
        }

        protected abstract string getExceptionMessage();

        public override string ToString()
        {
            return $"{GetType().Name}: {getExceptionMessage()}";
        }
    }

    [Serializable]
    public class ObjectNotFoundException : IdException
    {
        public ObjectNotFoundException() : base() { }
        public ObjectNotFoundException(string message) : base(message) { }
        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ObjectNotFoundException(Type type, int id) : base(type, id) { }

        protected override string getExceptionMessage()
        {
            return $"item #{Id} of type {Type.Name} not found";
        }
    }

    [Serializable]
    public class IdAlreadyExistsException : IdException
    {
        public IdAlreadyExistsException() : base() { }
        public IdAlreadyExistsException(string message) : base(message) { }
        public IdAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected IdAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public IdAlreadyExistsException(Type type, int id) : base(type, id) { }

        protected override string getExceptionMessage()
        {
            return $"item by id {Id} of type {Type.Name} already exists";
        }
    }
}
