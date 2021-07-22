using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User Not Found!")
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}