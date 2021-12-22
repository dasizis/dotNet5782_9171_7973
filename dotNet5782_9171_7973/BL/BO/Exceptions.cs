using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// An exception that occours when there is a trial to make an invalid action 
    /// in the BL layer
    /// </summary>
    [Serializable]
    public class InvalidActionException : Exception
    {
        public InvalidActionException() : base() { }
        public InvalidActionException(string message) : base(message) { }
        public InvalidActionException(string message, Exception inner) : base(message, inner) { }
        protected InvalidActionException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message;
        }
    }

    /// <summary>
    /// An exception that occours when the wanted item does not exist
    /// </summary>
    [Serializable]
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message) : base(message) { }

        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ObjectNotFoundException(Type type, Exception inner) : base($"item of type {type.Name} not found", inner) { }

        public ObjectNotFoundException(Type type) : base($"item of type {type.Name} not found") { }

    }

    /// <summary>
    /// An exception that occours when there is a try to add an object with existing id
    /// </summary>
    [Serializable]
    public class IdAlreadyExistsException : Exception
    {
        public IdAlreadyExistsException(string message) : base(message) { }

        public IdAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

        protected IdAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public IdAlreadyExistsException(Type type, Exception inner) : base($"item of type {type.Name} not found", inner) { }

        public IdAlreadyExistsException(Type type, int id) : base($"item by id {id} of type {type.Name} already exists") { }
    }

    /// <summary>
    /// An exception that occours when there is a try to add an object with existing id
    /// </summary>
    [Serializable]
    public class InvalidPropertyValueException : Exception
    {
        public string PropertyName { get; set; }

        public object Value { get; set; }

        public InvalidPropertyValueException(string message) : base(message) { }

        public InvalidPropertyValueException(string message, Exception inner) : base(message, inner) { }

        protected InvalidPropertyValueException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public InvalidPropertyValueException(string propName, object value)
        {
            PropertyName = propName;
            Value = value;
        }
    }

}
