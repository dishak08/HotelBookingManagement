using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Email Invalid")
        {
        }
    }
}
