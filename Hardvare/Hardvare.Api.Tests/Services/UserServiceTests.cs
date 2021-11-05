using System;
using Hardvare.Api.Models.QueryModels;
using Hardvare.Api.Repository;
using Hardvare.Api.Services;
using Moq;
using Xunit;

namespace Hardvare.Api.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async void WhenUserHasAdminRole_ThenTrueIsReturned()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUser(It.IsAny<Guid>()))
                .ReturnsAsync(this.GetAdminUser());
            var userService = new UserService(mockRepo.Object);

            // Act
            var isUserAnAdmin = await userService.IsUserAnAdmin(Guid.NewGuid());

            // Assert
            Assert.True(isUserAnAdmin);
        }

        [Fact]
        public async void WhenUserDoesNotHaveAdminRole_ThenFalseIsReturned()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUser(It.IsAny<Guid>()))
                .ReturnsAsync(this.GetCustomerUser);
            var userService = new UserService(mockRepo.Object);

            // Act
            var isUserAnAdmin = await userService.IsUserAnAdmin(Guid.NewGuid());

            // Assert
            Assert.False(isUserAnAdmin);
        }

        private User GetAdminUser()
        {
            return new User { Role = "ADMIN" };
        }

        private User GetCustomerUser()
        {
            return new User { Role = "CUSTOMER" };
        }
    }
}
