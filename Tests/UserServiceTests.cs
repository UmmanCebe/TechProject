using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TechCareer.Service.Concretes;
using TechCareer.DataAccess.Repositories.Abstracts;
using Core.Security.Entities;
using TechCareer.Service.Rules;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.Persistence.Extensions;

namespace Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<UserBusinessRules> _userBusinessRulesMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userBusinessRulesMock = new Mock<UserBusinessRules>(_userRepositoryMock.Object);
            _userService = new UserService(_userRepositoryMock.Object, _userBusinessRulesMock.Object);
        }

        [Test]
        public async Task GetAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Email = "test@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false, false, true, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);

            // Act
            var result = await _userService.GetAsync(u => u.Id == userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
        }

        

        [Test]
        public async Task AddAsync_ShouldAddUser_WhenEmailIsValid()
        {
            // Arrange
            var user = new User { Id = 1, Email = "newuser@example.com" };
            _userBusinessRulesMock.Setup(rule => rule.UserEmailShouldNotExistsWhenInsert(It.IsAny<string>()));
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.AddAsync(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
            _userBusinessRulesMock.Verify(rule => rule.UserEmailShouldNotExistsWhenInsert(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var user = new User { Id = 1, Email = "existinguser@example.com" };
            _userBusinessRulesMock.Setup(rule => rule.UserEmailShouldNotExistsWhenUpdate(It.IsAny<int>(), It.IsAny<string>()))
                                   .Throws(new BusinessException("User email already exists"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<BusinessException>(() => _userService.UpdateAsync(user));
            Assert.AreEqual("User email already exists", ex.Message);
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteUser()
        {
            // Arrange
            var user = new User { Id = 1, Email = "user@example.com" };
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<User>(), false)).ReturnsAsync(user);

            // Act
            var result = await _userService.DeleteAsync(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }
    }
}
