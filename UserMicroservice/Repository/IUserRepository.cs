using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.Repository
{
    public interface IUserRepository
    {
        public Task<AppUser> GetAppUser(int id);
        public Task<List<AppUser>> GetAllUsers();
        public Task<AppUser> InsertUser(AppUser user);
        public Task<AppUser> UpdateUser(AppUser user);
        public Task DeleteUser(int id);
        public Task<AuthTokenPayload> LoginUser(LoginRequest request);
    }
}
