using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Exceptions
{
    public class InvalidPhoneException : Exception
    {
        public InvalidPhoneException() : base("Phone Number Invalid")
        {
        }
    }
}
