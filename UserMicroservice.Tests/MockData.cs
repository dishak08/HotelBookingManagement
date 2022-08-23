using System;
using System.Collections.Generic;
using System.Text;
using UserMicroservice.Models;

namespace UserMicroservice.Tests
{
    class MockData
    {
        public static List<AppUser> GetAppUsers()
        {
            return new List<AppUser>{
                new AppUser{
                    Id = 1,
                    Name = "Need To Go Shopping",
                    Email = "naman",
                    Password = "1234",
                    Mobile="909009",
                    Role = Models.Enums.UserRole.ADMIN,
                    RegistrationDate=DateTime.Now
                },
                new AppUser{
                    Id = 2,
                    Name = "adii",
                    Email = "adi@",
                    Password = "1234",
                    Mobile="2364654",
                    Role = Models.Enums.UserRole.USER,
                    RegistrationDate=DateTime.Now
                },
                new AppUser{
                    Id = 3,
                    Name = "dfd",
                    Email = "adi@sd",
                    Password = "1234",
                    Mobile="909009",
                    Role = Models.Enums.UserRole.USER,
                    RegistrationDate=DateTime.Now
                }
         };
        }
    }
}
