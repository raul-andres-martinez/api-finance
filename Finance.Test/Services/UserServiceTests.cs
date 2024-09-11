using Finance.Domain.Errors;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
using Finance.Test.Mocks.Fixtures;
using Finance.Test.Mocks.Repositories;
using Finance.Test.Mocks.Services;
using Moq;
using Xunit;
using static Finance.Domain.Constants.Constant;

namespace Finance.Test.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async void AddUserAsync_NoDuplicateUser_ShouldSucceed()
        {
            // Arrange
            var userRequest = UserFixture.ValidUserRequest;
            var scenario = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(null)
                .AddSuccessfullCreateUser()
                .Build();

            var userService = UserServiceMock.Mock(scenario);

            // Act
            var result = await userService.AddUserAsync(userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(CustomActionResult.Created().StatusCode, result.StatusCode);

            Mock.Get(scenario).Verify(r => r.GetUserByEmailAsync(userRequest.Email), Times.Once);
            Mock.Get(scenario).Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void AddUserAsync_DuplicateUser_ShouldFail()
        {
            // Arrange
            var userRequest = UserFixture.ValidUserRequest;
            var scenario = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(userRequest)
                .Build();

            var userService = UserServiceMock.Mock(scenario);

            // Act
            var result = await userService.AddUserAsync(userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(UserError.EmailAlreadyInUse.StatusCode, result.StatusCode);
            Assert.Equal(UserError.EmailAlreadyInUse.Code, result.Error.Code);

            Mock.Get(scenario).Verify(r => r.GetUserByEmailAsync(userRequest.Email), Times.Once);
            Mock.Get(scenario).Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async void AddUserAsync_DatabaseError_ShouldFail()
        {
            // Arrange
            var userRequest = UserFixture.ValidUserRequest;
            var scenario = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(null)
                .AddUnsuccessfullCreateUser()
                .Build();

            var userService = UserServiceMock.Mock(scenario);

            // Act
            var result = await userService.AddUserAsync(userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(UserError.FailedToCreate.StatusCode, result.StatusCode);
            Assert.Equal(UserError.FailedToCreate.Code, result.Error.Code);

            Mock.Get(scenario).Verify(r => r.GetUserByEmailAsync(userRequest.Email), Times.Once);
            Mock.Get(scenario).Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void LoginAsync_ValidUser_ShouldSucceed()
        {
            // Arrange
            (var user, var login) = UserFixture.JohnDoeLogin;
            var scenario = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(user)
                .Build();

            var userService = UserServiceMock.Mock(scenario);

            // Act
            var result = await userService.LoginAsync(login);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.True(result.Success);
            Assert.False(string.IsNullOrEmpty(result.Value.Token));
            Assert.Equal(TokenType.Bearer, result.Value.TokenType);

            Mock.Get(scenario).Verify(r => r.GetUserByEmailAsync(user.Email), Times.Once);
        }

        [Fact]
        public async void LoginAsync_WrongPasswordOrEmail_ShouldFail()
        {
            // Arrange
            var login = UserFixture.ValidLoginRequest;
            var user = UserFixture.ValidUsers(1).FirstOrDefault();
            var scenario = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(user)
                .Build();

            var userService = UserServiceMock.Mock(scenario);

            // Act
            var result = await userService.LoginAsync(login);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(UserError.InvalidLogin.StatusCode, result.StatusCode);
            Assert.Equal(UserError.InvalidLogin.Code, result.Error.Code);

            Mock.Get(scenario).Verify(r => r.GetUserByEmailAsync(login.Email), Times.Once);
        }

        [Fact]
        public async void LoginAsync_UserNotFound_ShouldFail()
        {
            // Arrange
            var login = UserFixture.ValidLoginRequest;
            var scenario = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(null)
                .Build();

            var userService = UserServiceMock.Mock(scenario);

            // Act
            var result = await userService.LoginAsync(login);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(UserError.InvalidLogin.StatusCode, result.StatusCode);
            Assert.Equal(UserError.InvalidLogin.Code, result.Error.Code);

            Mock.Get(scenario).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
        }
    }
}