using Finance.Domain.Interfaces.Repositories;
using Finance.Service.Services;
using Finance.Test.Mocks.Configs;

namespace Finance.Test.Mocks.Services
{
    public static class ExpenseServiceMock
    {
        public static ExpenseService Mock(IUserRepository userRepositoryMock, IExpenseRepository expenseRepositoryMock)
        {
            var expenseService = new ExpenseService(userRepositoryMock, expenseRepositoryMock, MapperConfigs.Setup());
            return expenseService;
        }
    }
}