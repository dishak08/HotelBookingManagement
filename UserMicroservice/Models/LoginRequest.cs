using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginRequest() { }
        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
