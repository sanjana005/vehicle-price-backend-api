using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using vehicle_price_backend_api.Controllers;
using vehicle_price_backend_api.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using vehicle_price_backend_api.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace vehicle_price_unit_testing
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller;
        private UserAPIDbCon _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UserAPIDbCon>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new UserAPIDbCon(options);
            _controller = new UserController(_dbContext);
        }

        [Test]
        public async Task UserRegistration_ValidInput_ReturnsOkResult()
        {

            var registrationDTO = new UserRegistrationDTO
            {
                Name = "TestUser",
                Email = "test@example.com",
                Password = "password",
                PhoneNo = "1234567890",
                UserType = "Regular"
            };


            var result = await _controller.UserRegistration(registrationDTO);


            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task UserLogin_ValidCredentials_ReturnsOkResult()
        {

            var loginDTO = new UserLoginDTO
            {
                Email = "admin@gmail.com",
                Password = "Admin123",
                UserType = "Admin"
            };


            var result = await _controller.UserLogin(loginDTO);


            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
