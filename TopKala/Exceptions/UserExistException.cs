using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Exceptions
{
    public class UserExistException : Exception
    {
        public UserExistException() : base("User Exists!")
        {
        }

        public UserExistException(string message) : base(message)
        {
        }

        public UserExistException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}