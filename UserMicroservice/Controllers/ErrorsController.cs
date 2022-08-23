using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.Controllers
{
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [HttpGet("/error")]
        public IActionResult Error()
        {

            return Ok();
        }
    }
}
