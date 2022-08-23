using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using UserMicroservice.Exceptions;
using UserMicroservice.Models;
using UserMicroservice.Services;

namespace UserMicroservice.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Database _db;
        private readonly IAuthorizationService_Api _authApi;

        public UserRepository(Database db, IAuthorizationService_Api authApi)
        {
            _db = db;
            _authApi = authApi;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _db.AppUsers.FindAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            _db.Entry(user).State = EntityState.Deleted;
            _db.AppUsers.Remove(user);

            await _db.SaveChangesAsync();
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            return await _db.AppUsers.ToListAsync() ?? new List<AppUser>();
        }

        public async Task<AppUser> GetAppUser(int id)
        {
            return await _db.AppUsers.FindAsync(id);
        }

        public async Task<AppUser> InsertUser(AppUser user)
        {
            var hasher = new PasswordHasher<AppUser>();
            user.Password = hasher.HashPassword(user, user.Password);

            await _db.AppUsers.AddAsync(user);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return user;
        }

        public async Task<AuthTokenPayload> LoginUser(LoginRequest request)
        {
            AppUser user = await _db.AppUsers.Where(u => u.Email == request.Email).FirstOrDefaultAsync();

            if (user == default)
            {
                throw new UserNotFoundException();
            }

            var hasher = new PasswordHasher<AppUser>();

            var verify = hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if(verify == PasswordVerificationResult.Failed)
            {
                throw new UserNotFoundException(message: "Password doesnot match");
            }

            return await _authApi.GetAuthTokenAsync(user);
        }

        public async Task<AppUser> UpdateUser(AppUser user)
        {
            if(await _db.AppUsers.FindAsync(user.Id) == null) return null;

            var hasher = new PasswordHasher<AppUser>();
            user.Password = hasher.HashPassword(user, user.Password);

            _db.Entry(user).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return user;
        }


    }
}
