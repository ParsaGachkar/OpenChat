using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class LoginFaildExeption : Exception
    {
        public LoginFaildExeption()
        {
        }

        public LoginFaildExeption(string message) : base(message)
        {
        }

        public LoginFaildExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoginFaildExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}