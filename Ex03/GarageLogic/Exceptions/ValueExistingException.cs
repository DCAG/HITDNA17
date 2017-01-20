using System;
using System.Runtime.Serialization;

namespace GarageLogic
{
    [Serializable]
    internal class ValueExistingException : Exception
    {
        public ValueExistingException()
        {
        }

        public ValueExistingException(string message) : base(message)
        {
        }

        public ValueExistingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueExistingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}