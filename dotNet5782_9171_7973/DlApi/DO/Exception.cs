using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message) : base(message) { }

        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ObjectNotFoundException(Type type, Exception inner)  : base($"item of type {type.Name} not found", inner) {}
    }

    [Serializable]
    public class IdAlreadyExistsException : Exception
    {
        public IdAlreadyExistsException(string message) : base(message) { }

        public IdAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

        protected IdAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public IdAlreadyExistsException(Type type, Exception inner) : base($"item of type {type.Name} not found", inner) { }

        public IdAlreadyExistsException(Type type, int id) : base($"item by id {id} of type {type.Name} already exists") { }
    }
}
