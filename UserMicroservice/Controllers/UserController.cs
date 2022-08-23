using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Swashbuckle.AspNetCore.Annotations;

using UserMicroservice.Models;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Repository;
using UserMicroservice.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
#nullable enable
namespace UserMicroservice.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;


        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            this._logger = logger;
            this._userRepository = userRepository;
        }

        // GET: /user
        [SwaggerResponse(200, "Fetched Users", typeof(Response<List<AppUser>>))]
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [HttpGet, Authorize]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all users");
            return Ok(new Response<List<AppUser>>(
                    "Users Found", 
                    true, 
                    await _userRepository.GetAllUsers()
                ));
        }

        // GET /user/5
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [SwaggerResponse(200, "Fetched User Details", typeof(Response<AppUser>))]
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation("Getting list of a particular user");
            AppUser user = await _userRepository.GetAppUser(id);

            if (user == null) 
            {
                _logger.LogInformation("User with id {0} not found", id);
                return NotFound(new Response("User not found", false));
            }

            return Ok(new Response<AppUser>("User found", true, user));
        }

        // POST /user/
        [SwaggerResponse(501, "Server Error", typeof(Response))]
        [SwaggerResponse(201, "Created", typeof(Response<AppUser>))]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AppUser user)
        {
            AppUser insertedUser = await _userRepository.InsertUser(user);
            _logger.LogInformation("Added a new user");
            return CreatedAtAction(nameof(Get), new { id = user.Id }, new Response<AppUser>("User Added", true, insertedUser));
        }

        // PUT /user/5
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(404, "Not Found", typeof(Response))]
        [SwaggerResponse(202, "User Updated", typeof(Response<AppUser>))]
        [HttpPut("{id}"), Authorize()]
        public async Task<ActionResult> Put(int id, [FromBody] AppUser user)
        {
            _logger.LogInformation("Ids dont match with eachother");
            if (user.Id != id) return BadRequest(new Response("Ids Dont match", false));

            _logger.LogInformation("Updating information of user with id {}", id);
            AppUser updatedUser = await _userRepository.UpdateUser(user);

            _logger.LogInformation("User with id {} is not found", id);
            if (updatedUser == null) return NotFound(new Response("User Not Found", false));

            return Accepted(new Response<AppUser>("User Data Updated", true, updatedUser));
        }

        // DELETE /user/5
        [SwaggerResponse(200, "Get Authentication Token", typeof(Response))]
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting user with id {}", id);
            await _userRepository.DeleteUser(id);

            return Ok(new Response("User Deleted", true));
        }

        // POST /user/login
        [SwaggerResponse(200, "Get Authentication Token", typeof(Response<AuthTokenPayload?>))]
        [SwaggerResponse(401, "Unautherized", typeof(Response))]
        [SwaggerResponse(502, "Internal Server Error", typeof(Response))]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest login)
        {
            _logger.LogInformation("logging in user");
            AuthTokenPayload token = await _userRepository.LoginUser(login);
            return Ok(new Response<AuthTokenPayload>("Logged in Successfully", true, token));
        }
    }
}
