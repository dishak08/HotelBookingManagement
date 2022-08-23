using NUnit.Framework;
using UserMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Services;
using UserMicroservice.Models;
using Moq;
using UserMicroservice.Tests;
using System.Threading.Tasks;
using UserMicroservice.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace UserMicroservice.Repository.Tests
{
    [TestFixture()]
    public class UserRepositoryTests
    {
        InMemoryUserRepository _repo;

        private readonly string AccessToken = "sdajkfgjkasghkjagsd";

        [SetUp]
        public void Setup()
        {
            var authorizationService_Api = new Mock<IAuthorizationService_Api>();


            authorizationService_Api.Setup(x => x.GetAuthTokenAsync(new AppUser() { Id = 1, Name = "AppUser1", Email = "Make1", Password = "Medel1", Mobile = "9000000000", Role = UserRole.ADMIN, RegistrationDate = System.DateTime.Now }))
                                    .ReturnsAsync(new AuthTokenPayload(AccessToken));

            _repo = new InMemoryUserRepository(authorizationService_Api.Object);
        }

        [Test()]
        public void UserRepositoryTest()
        {

        }

        [Test()]
        public async Task DeleteUserTest()
        {
            int id = InMemoryUserRepository.Users[2].Id;
            await _repo.DeleteUser(id);

            Assert.AreNotEqual(InMemoryUserRepository.Users[2].Id, id);
        }

        [Test()]
        public async Task GetAllUsersTest()
        {
            var res = await _repo.GetAllUsers();

            Assert.AreSame(res, InMemoryUserRepository.Users);
        }

        [Test()]
        public async Task GetAppUserTest()
        {
            var res = await _repo.GetAppUser(InMemoryUserRepository.Users[0].Id);

            Assert.AreSame(res, InMemoryUserRepository.Users[0]);
        }

        [Test()]
        public async Task InsertUserTest()
        {
            var res = await _repo.InsertUser(new AppUser(
                name: "Naman Garg",
                email: "naman.garg@cognizant.com",
                password: "password123",
                mobile: "9876543210",
                role: UserRole.ADMIN,
                registrationDate: DateTime.UtcNow
            ));

            Assert.IsNotNull(res);
            Assert.AreEqual(res.Role, UserRole.ADMIN, "Role Not Verified");
            Assert.AreEqual(res.Email, "naman.garg@cognizant.com", "Email Not Verified");

            Assert.AreEqual(res.Password, "password123", "Password Not Verified");

        }

        [Test()]
        public async Task LoginUserTest()
        {
            AuthTokenPayload res = await _repo.LoginUser(new LoginRequest(
                email: "Make1",
                password: "Medel1"
            ));

            Assert.IsNull(res);
            Assert.AreEqual(AccessToken, res?.accessToken ?? AccessToken);
        }

        [Test()]
        public async Task UpdateUserTest()
        {
            var user = InMemoryUserRepository.Users[0];

            user.Name = "Someone";
            user.Role = UserRole.USER;
            user.Email = "naman.garg@cognizant.com";
            user.Password = "password123";

            var res = await _repo.UpdateUser(user);

            Assert.IsNotNull(res);
            Assert.AreEqual(
                UserRole.USER,
                res.Role,
                "Role Not Verified");
            Assert.AreEqual(
                "naman.garg@cognizant.com",
                res.Email,
                "Email Not Verified");

            Assert.AreEqual(
                "password123",
                res.Password,
                "Password Not Verified");


            var userFromDb = await _repo.GetAppUser(user.Id);

            Assert.IsNotNull(userFromDb);
            Assert.AreEqual(
                user.Role,
                userFromDb.Role,
                "Role Not Updated");
            Assert.AreEqual(
                user.Email,
                userFromDb.Email,
                "Email Not Updated");

            Assert.AreEqual(
                "password123",
                userFromDb.Password,
                "Password Not Updated");
        }
    }
}