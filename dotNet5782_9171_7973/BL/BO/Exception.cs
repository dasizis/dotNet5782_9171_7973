using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class InValidActionException : Exception
    {
        public InValidActionException() : base() { }
        public InValidActionException(string message) : base(message) { }
        public InValidActionException(string message, Exception inner) : base(message, inner) { }
        protected InValidActionException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message;
        }
    }

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

        protected abstract string GetExceptionMessage();

        public override string ToString()
        {
            return $"{GetType().Name}: {GetExceptionMessage()}";
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

        protected override string GetExceptionMessage()
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

        protected override string GetExceptionMessage()
        {
            return $"item by id {Id} of type {Type.Name} already exists";
        }
    }
}
