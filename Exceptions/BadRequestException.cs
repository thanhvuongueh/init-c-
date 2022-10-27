using System.Runtime.Serialization;

namespace InitalWebAPI.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException()
            : base()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }
        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
