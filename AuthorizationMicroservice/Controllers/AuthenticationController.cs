using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        public readonly AuthenticationService _authService;
        /*public readonly ILogger _logger;*/

        public AuthenticationController(AuthenticationService _auth)
        {
            _authService = _auth;
        }


        [HttpPost("login")]
        public ActionResult Login([FromBody] AppUser user)
        {
            return Ok(_authService.GetAuthToken(user));
        }
    }
}
