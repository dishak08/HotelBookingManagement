using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models.Enums;

namespace UserMicroservice.Models
{
    public class AppUser
    {
        public AppUser()
        {
        }

        public AppUser(string name, string email, string password, string mobile, DateTime? registrationDate, UserRole role = UserRole.USER)
        {
            Name = name;
            Email = email;
            Password = password;
            Mobile = mobile;
            Role = role;
            RegistrationDate = registrationDate ?? DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }

    }
}
