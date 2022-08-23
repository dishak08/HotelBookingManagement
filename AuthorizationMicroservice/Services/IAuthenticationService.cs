using AuthorizationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Services
{
    public interface IAuthenticationService
    {
        public AuthTokenPayload GetAuthToken(AppUser user);
    }
}
