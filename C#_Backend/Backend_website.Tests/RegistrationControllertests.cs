using Backend_website.Controllers;
using Backend_website.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Backend_website.Tests
{
    public class RegistrationControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly RegistrationController _controller;

        public RegistrationControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config.GetConnectionString("DBConnection")).Returns("YourConnectionString");

            _controller = new RegistrationController(_mockConfiguration.Object);
        }

        [Fact]
        public void Register_ValidRegistration_ReturnsOk()
        {
            var registration = new Registration
            {
                FirstName = "Muhammad",
                LastName = "Muhammad",
                Email = "muhammad@gmail.com",
                Password = "muhammad",
                IsActive = true,
                Role = "user"
            };

            var result = _controller.Register(registration);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Data Inserted Successfully", okResult.Value);
        }

        [Fact]
        public void Register_MissingFields_ReturnsBadRequest()
        {
            var registration = new Registration
            {
                FirstName = "Muhammad",
                LastName = "Muhammad",
                Email = "muhammad",
                Password = "000",
                IsActive = false
            };

            var result = _controller.Register(registration);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Register_ThrowsException_ReturnsServerError()
        {
            var registration = new Registration
            {
                FirstName = "khan",
                LastName = "khan",
                Email = "khan@gmail.com",
                Password = "khan123",
                IsActive = true,
                Role = "user"
            };

            _mockConfiguration.Setup(config => config.GetConnectionString("DBConnection")).Throws(new System.Exception("Database error"));

            var result = _controller.Register(registration);

            var serverError = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, serverError.StatusCode);
            Assert.Contains("Internal server error", serverError.Value.ToString());
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsOkWithRole()
        {
            var email = "ahmedkamal@gmail.com";
            var password = "ahmed01";

            var result = _controller.Login(email, password);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Login Successful", okResult.Value.ToString());
        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var email = "ahmad_kamal@gmail.com";
            var password = "ahmad@1";

            var result = _controller.Login(email, password);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid email or password", unauthorizedResult.Value);
        }
    }
}
