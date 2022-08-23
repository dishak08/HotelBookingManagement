using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Exceptions
{
    public class ConnectedServiceException : Exception
    {
        public ConnectedServiceException() : base("Error in Connecting Service")
        {
        }
    }
}
