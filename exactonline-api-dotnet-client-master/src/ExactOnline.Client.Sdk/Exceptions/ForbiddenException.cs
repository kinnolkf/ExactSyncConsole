using System;

namespace ExactOnline.Client.Sdk.Exceptions
{
    [Serializable]
    public class ForbiddenException : Exception // HTTP: 403 
    {
        public ForbiddenException() { }
        public ForbiddenException(string message) : base(message) { }
        public ForbiddenException(string message, Exception inner) : base(message, inner) { }
        protected ForbiddenException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}