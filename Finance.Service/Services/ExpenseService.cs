using AutoMapper;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;

namespace Finance.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IUserRepository userRepository, IExpenseRepository expenseRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult> AddExpenseAsync(string? userEmail, ExpenseRequest request)
        {
            var userResult = await GetUserByEmailAsync(userEmail);

            if (!userResult.Success)
            {
                return userResult.GetError();
            }

            var user = userResult.GetValue();
            var entity = _mapper.Map<Expense>(request);
            entity.UserId = user.Uid;

            var result = await _expenseRepository.AddExpenseAsync(entity);

            return result;
        }

        public async Task<CustomActionResult<List<ExpenseResponse>>> GetFilteredExpensesAsync(string? userEmail, ExpensesFilterRequest request)
        {
            var userResult = await GetUserByEmailAsync(userEmail);

            if (!userResult.Success)
            {
                return userResult.GetError();
            }

            var user = userResult.GetValue();
            var expenses = await _expenseRepository.GetFilteredExpensesAsync(user.Uid, request);

            if (expenses.GetValue().Count == 0)
            {
                return CustomActionResult<List<ExpenseResponse>>.NoContent();
            }

            var expenseResponse = _mapper.Map<List<ExpenseResponse>>(expenses.GetValue());

            return expenseResponse;
        }

        public async Task<CustomActionResult<ExpenseResponse>> GetExpenseAsync(string? userEmail, string id)
        {
            var userResult = await GetUserByEmailAsync(userEmail);

            if (!userResult.Success)
            {
                return userResult.GetError();
            }

            var expense = await _expenseRepository.GetExpenseAsync(Guid.Parse(id));

            if (expense.Value is null)
            {
                return CustomActionResult<ExpenseResponse>.NoContent();
            }

            var expenseResponse = _mapper.Map<ExpenseResponse>(expense.GetValue());

            return expenseResponse;
        }

        public async Task<CustomActionResult> DeleteExpenseAsync(string? userEmail, string id)
        {
            var userResult = await GetUserByEmailAsync(userEmail);

            if (!userResult.Success)
            {
                return userResult.GetError();
            }

            var expense = await _expenseRepository.DeleteExpenseAsync(Guid.Parse(id));

            return expense;
        }

        private async Task<CustomActionResult<User>> GetUserByEmailAsync(string? userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return ExpenseError.InvalidUser;
            }

            var user = await _userRepository.GetUserByEmailAsync(userEmail);

            if (!user.Success)
            {
                return ExpenseError.InvalidUser;
            }

            return user;
        }
    }
}