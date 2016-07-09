using System;

namespace ExactOnline.Client.Sdk.Exceptions
{
    [Serializable]
    public class InternalServerErrorException : Exception // HTTP 500
    {
        public InternalServerErrorException() { }
        public InternalServerErrorException(string message) : base(message) { }
        public InternalServerErrorException(string message, Exception inner) : base(message, inner) { }
        protected InternalServerErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}