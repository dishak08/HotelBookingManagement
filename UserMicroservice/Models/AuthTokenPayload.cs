using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class AuthTokenPayload
    {
        public string accessToken { get; set; }

        public AuthTokenPayload() { }

        public AuthTokenPayload(string accessToken)
        {
            this.accessToken = accessToken;
        }
    }
}
