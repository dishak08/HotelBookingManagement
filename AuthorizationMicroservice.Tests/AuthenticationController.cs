using NUnit.Framework;
using AuthorizationMicroservice;
using AuthorizationMicroservice.Controllers;
using AuthorizationMicroservice.Services;
using Microsoft.Extensions.Logging;
using Moq;
using AuthorizationMicroservice.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Tests
{
   
    public class Tests
    {

        AuthenticationController auth;
        AppUser user = new AppUser
        {
            Id = 4,
            Name = "Ayush Raj Vaish",
            Email = "ayush.vaish@cognizant.com",
            Mobile = "9876543210",
            Role = Models.Enums.UserRole.USER,
            RegistrationDate = DateTime.UtcNow
        };


        [SetUp]
        
        public void Setup()
        {
            var authRepoMoq = new Mock<IAuthenticationService>();
            var loggerMoq = new Mock<ILogger<AuthenticationController>>();

            authRepoMoq.Setup(x => x.GetAuthToken(user))
                .Returns(new AuthTokenPayload
                {
                    AccessToken = "1234"
                });
        }

        [Test()]
        public void Test1()
        {
            var response = (OkObjectResult)auth.Login(user);

            Assert.AreEqual(response.StatusCode, 200);
        }

    }
}