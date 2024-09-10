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
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return ExpenseError.InvalidUser;
            }

            var user = await _userRepository.GetUserByEmailAsync(userEmail);

            if (!user.Success)
            {
                return ExpenseError.InvalidUser;
            }

            var entity = _mapper.Map<Expense>(request);
            entity.UserId = user.Value.Uid;

            var result = await _expenseRepository.AddExpenseAsync(entity);

            return result;
        }

        public async Task<CustomActionResult<List<ExpenseResponse>>> GetFilteredExpensesAsync(string userEmail, ExpensesFilterRequest request)
        {
            throw new NotImplementedException();

            //var user = await GetUserAsync(userEmail);

            //if (!user.Success || user.Value == null)
            //{
            //    return Result.Failure<List<ExpenseResponse>>(user.Error, user.ErrorCode);
            //}

            //var expenses = await _expenseRepository.GetFilteredExpensesAsync(userEmail, request);

            //if (expenses == null)
            //{
            //    return Result.Ok(new List<ExpenseResponse>());
            //}

            //var expenseResponse = _mapper.Map<List<ExpenseResponse>>(expenses);

            //if (expenseResponse == null)
            //{
            //    return Result.Failure<List<ExpenseResponse>>("Failed to map expenses.", ErrorCode.INTERNAL_ERROR);
            //}

            //return Result.Ok(expenseResponse);
        }
    }
}
