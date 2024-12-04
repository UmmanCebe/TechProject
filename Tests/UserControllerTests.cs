using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TechCareer.API.Controllers;
using TechCareer.Service.Abstracts;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Persistence.Extensions;
using System.Linq.Expressions;

namespace Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private UsersController _controller;

        [SetUp]
        public void SetUp()
        {
            // Mocking IUserService
            _mockUserService = new Mock<IUserService>();

            // Controller'ı başlatıyoruz
            _controller = new UsersController(_mockUserService.Object);
        }

        [Test]
        public async Task GetAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Email = "test@example.com" };

            _mockUserService.Setup(service => service.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false, false, true, CancellationToken.None))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetAsync(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task GetAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 1;
            _mockUserService.Setup(service => service.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false, false, true, CancellationToken.None))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.GetAsync(userId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("User not found.", notFoundResult.Value);
        }


        [Test]
        public async Task GetListAsync_ShouldReturnUserList()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Email = "test@example.com" } };
            _mockUserService.Setup(service => service.GetListAsync(It.IsAny<Expression<Func<User, bool>>>(), null, false, false, true, CancellationToken.None))
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetListAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(users, okResult.Value);
        }

        [Test]
        public async Task AddAsync_ShouldReturnCreatedUser_WhenModelIsValid()
        {
            // Arrange
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserService.Setup(service => service.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.AddAsync(user);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdResult = result as CreatedAtActionResult;
            Assert.AreEqual(user, createdResult.Value);
            Assert.AreEqual("GetAsync", createdResult.ActionName);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnUpdatedUser_WhenIdMatches()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Email = "updated@example.com" };

            _mockUserService.Setup(service => service.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.UpdateAsync(userId, user);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnDeletedUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Email = "test@example.com" };

            _mockUserService.Setup(service => service.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false, false, true, CancellationToken.None))
                .ReturnsAsync(user);
            _mockUserService.Setup(service => service.DeleteAsync(It.IsAny<User>(), false))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.DeleteAsync(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 1;
            _mockUserService.Setup(service => service.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false, false, true, CancellationToken.None))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.DeleteAsync(userId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("User not found.", notFoundResult.Value);
        }
    }
}
