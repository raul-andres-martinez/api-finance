using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Configs;
using Finance.Service.Services;
using Finance.Test.Mocks.Configs;

namespace Finance.Test.Mocks.Services
{
    public static class UserServiceMock
    {
        public static UserService Mock(IUserRepository mockedScenario)
        {
            var repositoryMock = mockedScenario;
            AppConfigMock.Mock();
            var userService = new UserService(repositoryMock, AppConfig.Instance);
            return userService;
        }
    }
}