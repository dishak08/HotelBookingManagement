using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool Status { get; set; }

        public Response() { }

        public Response(string message, bool status)
        {
            Message = message;
            Status = status;
        }
    }

    public class Response<T> : Response
    {
        public T Payload { get; set; }

        
        public Response(string message, bool status, T payload)
        {
            Message = message;
            Status = status;
            Payload = payload;
        }
    }
}
