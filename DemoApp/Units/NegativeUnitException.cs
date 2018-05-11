using System;
using System.Runtime.Serialization;

namespace Celestial.Units
{
    [Serializable]
    internal class NegativeUnitException : Exception
    {
        public NegativeUnitException() : this("Unit cannot be negative")
        {
            
        }

        public NegativeUnitException(string message) : base(message)
        {
        }

        public NegativeUnitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NegativeUnitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}