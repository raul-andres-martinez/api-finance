using AutoMapper;
using Finance.Domain.Dtos;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Enum;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Entities;

namespace Finance.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IUserService userService, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Result> AddExpenseAsync(string userEmail, ExpenseRequest request)
        {
            var user = await GetUserAsync(userEmail);

            if (!user.Success || user.Value == null) 
            {
                return user;
            }

            var entity = request.ToEntity(user.Value.Uid);
            var result = await _expenseRepository.AddExpense(entity);

            return result ? 
                Result.Ok() :
                Result.Failure("Failed to add new expense.", ErrorCode.DATABASE_ERROR);
        }

        public async Task<Result<List<ExpenseResponse>>> GetFilteredExpensesAsync(string userEmail, ExpensesFilterRequest request)
        {
            var user = await GetUserAsync(userEmail);

            if (!user.Success || user.Value == null)
            {
                return Result.Failure<List<ExpenseResponse>>(user.Error, user.ErrorCode);
            }

            var expenses = await _expenseRepository.GetFilteredExpensesAsync(userEmail, request);

            if (expenses == null)
            {
                return Result.Ok(new List<ExpenseResponse>());
            }

            var expenseResponse = _mapper.Map<List<ExpenseResponse>>(expenses);

            if (expenseResponse == null)
            {
                return Result.Failure<List<ExpenseResponse>>("Failed to map expenses.", ErrorCode.INTERNAL_ERROR);
            }

            return Result.Ok(expenseResponse);
        }

        private async Task<Result<User>> GetUserAsync(string userEmail)
        {
            var user = await _userService.GetUserByEmailAsync(userEmail);

            if (!user.Success || user.Value == null)
            {
                return Result.Failure<User>(user.Error, user.ErrorCode);
            }

            return Result.Ok(user.Value);
        }
    }
}
