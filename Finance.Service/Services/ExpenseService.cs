using Finance.Domain.Dtos;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Interfaces.Services;

namespace Finance.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async Task<bool> AddExpenseAsync(ExpenseRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
