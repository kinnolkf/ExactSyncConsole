using System;

namespace ExactOnline.Client.Sdk.Exceptions
{
    [Serializable]
    public class UnauthorizedException : Exception // HTTP: 401
    {
        public UnauthorizedException() { }
        public UnauthorizedException(string message) : base(message) { }
        public UnauthorizedException(string message, Exception inner) : base(message, inner) { }
        protected UnauthorizedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}