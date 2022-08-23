using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;
namespace UserMicroservice.Services
{
    public interface IAuthorizationService_Api
    {
        public Task<AuthTokenPayload> GetAuthTokenAsync(AppUser user);
    }
}
