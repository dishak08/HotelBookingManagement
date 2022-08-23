using NUnit.Framework;
using UserMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using UserMicroservice.Models;
using UserMicroservice.Models.Enums;

namespace UserMicroservice.Services.Tests
{
    [TestFixture()]
    public class AuthorizationService_ApiTests
    {
        AuthorizationService_Api _service;

        [SetUp()]
        public void Setup()
        {
            var configuration = new Mock<IConfiguration>();

            configuration.SetupGet(m => m[It.Is<string>(s => s == "ConnectedServices:Authorization")]).Returns("https://localhost:44771/");


            HttpClient client = new HttpClient();

            _service = new AuthorizationService_Api(client, configuration.Object);
        }

        [Test()]
        public async Task GetAuthTokenAsyncTest()
        {
            var res = await _service.GetAuthTokenAsync(new AppUser { Id = 1, Name = "AppUser1", Email = "Make1", Password = "Medel1", Mobile = "9000000000", Role = UserRole.ADMIN, RegistrationDate = System.DateTime.Now });

            Assert.IsInstanceOf<AuthTokenPayload>(res);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<string>(res.accessToken);
        }
    }
}