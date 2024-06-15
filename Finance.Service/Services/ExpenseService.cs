using Finance.Domain.Dtos;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Interfaces.Services;

namespace Finance.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserService _userService;

        public ExpenseService(IExpenseRepository expenseRepository, IUserService userService)
        {
            _expenseRepository = expenseRepository;
            _userService = userService;
        }

        public async Task<Result> AddExpenseAsync(string userEmail, ExpenseRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(userEmail);

            if (!user.Success)
            {
                return Result.Failure(user.Error);
            }

            var entity = request.ToEntity(user.Value.Uid);
            var result = await _expenseRepository.AddExpense(entity);

            return result ? Result.Failure("Failed to add new expense.") :
                Result.Ok();
        }
    }
}
