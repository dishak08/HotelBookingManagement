using NUnit.Framework;
using Moq;
using UserMicroservice.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UserMicroservice.Repository;
using Microsoft.Extensions.Logging;
using UserMicroservice.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Tests;

namespace UserMicroservice.Controllers.Tests
{
    [TestFixture()]
    public class UserControllerTests
    {
        UserController controller;

        int Id = 1;
        AppUser user = new AppUser
        {
            Id = 4,
            Name = "Ayush Raj Vaish",
            Email = "ayush.vaish@cognizant.com",
            Password = "password123",
            Mobile = "9876543210",
            Role = Models.Enums.UserRole.USER,
            RegistrationDate = DateTime.UtcNow
        };

        [SetUp]
        public void Setup()
        {
            var userRepoMoq = new Mock<IUserRepository>();
            var loggerMoq = new Mock<ILogger<UserController>>();
            var users = MockData.GetAppUsers();

            userRepoMoq.Setup(x => x.GetAllUsers())
                .ReturnsAsync(users);

            userRepoMoq.Setup(x => x.GetAppUser(Id))
                .ReturnsAsync(users.Where(u => u.Id == Id).FirstOrDefault());

            userRepoMoq.Setup(x => x.InsertUser(user))
                .ReturnsAsync(user);

            userRepoMoq.Setup(x => x.UpdateUser(user))
                .ReturnsAsync(user);

            userRepoMoq.Setup(x => x.DeleteUser(Id))
                .Returns(Task.CompletedTask);

            userRepoMoq.Setup(x => x.LoginUser(new LoginRequest { Email = "ayush.vaish@cognizant.com", Password = "password123" }))
                .ReturnsAsync(new AuthTokenPayload { 
                    accessToken = "1234556"
                });

            controller = new UserController(loggerMoq.Object, userRepoMoq.Object);
        }

        [Test()]
        public async Task UserControllerTest()
        {
            var response = (OkObjectResult) await controller.Get();

            Assert.AreEqual(response.StatusCode, 200);


        }

        [Test()]
        public async Task GetTest()
        {
            var response = (OkObjectResult) await controller.Get();

            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test()]
        public async Task GetTest1()
        {
            var response = (OkObjectResult) await controller.Get(Id);

            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test()]
        public async Task PostTest()
        {
            var response = (CreatedAtActionResult) await controller.Post(user);

            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test()]
        public async Task PutTest()
        {
            var response = (AcceptedResult) await controller.Put(user.Id, user);

            Assert.AreEqual(response.StatusCode, 202);
        }

        [Test()]
        public async Task DeleteTest()
        {
            var response = (OkObjectResult) await controller.Delete(Id);

            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test()]
        public async Task LoginTest()
        {
            var response = (OkObjectResult) await controller.Login(new LoginRequest { Email = "ayush.vaish@cognizant.com", Password = "password123" });

            Assert.AreEqual(response.StatusCode, 200);
        }
    }
}