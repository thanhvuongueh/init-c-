using System.Runtime.Serialization;

namespace InitalWebAPI.Exceptions
{
    [Serializable]
    public class DBRecordNotFoundException : Exception
    {
        public DBRecordNotFoundException()
            : base()
        {
        }

        public DBRecordNotFoundException(string message)
            : base(message)
        {
        }
        public DBRecordNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DBRecordNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
