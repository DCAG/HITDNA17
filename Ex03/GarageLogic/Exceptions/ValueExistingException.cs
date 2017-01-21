using System;

namespace GarageLogic
{
    public class ValueExistingException : Exception
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
    }
}