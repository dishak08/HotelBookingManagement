using AuthorizationMicroservice.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Models
{
    public class AppUser
    {
        public AppUser()
        {
        }

        public AppUser(int id, string name, string email, string mobile, DateTime registrationDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Mobile = mobile;
            RegistrationDate = registrationDate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }
    }
}
