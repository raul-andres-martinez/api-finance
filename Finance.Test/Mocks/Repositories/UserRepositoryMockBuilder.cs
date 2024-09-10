using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
using Moq;

namespace Finance.Test.Mocks.Repositories
{
    public class UserRepositoryMockBuilder
    {
        private readonly Mock<IUserRepository> repositoryMock = new();

        public UserRepositoryMockBuilder AddGetUserByEmail(User? user)
        {
            repositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) =>
                    user != null && user.Email == email ? user : UserError.NotFound);
            return this;
        }

        public UserRepositoryMockBuilder AddSuccessfullCreateUser()
        {
            repositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .ReturnsAsync(CustomActionResult.Created);
            return this;
        }

        public UserRepositoryMockBuilder AddUnsuccessfullCreateUser()
        {
            repositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .ReturnsAsync(UserError.FailedToCreate);
            return this;
        }

        public IUserRepository Build() => repositoryMock.Object;
    }
}