using AutoMapper;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;

namespace Finance.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ExpenseService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CustomActionResult> AddExpenseAsync(string userEmail, ExpenseRequest request)
        {
            throw new NotImplementedException();

            //var user = await GetUserAsync(userEmail);

            //if (!user.Success || user.Value == null) 
            //{
            //    return user;
            //}

            //var entity = request.ToEntity(user.Value.Uid);
            //var result = await _expenseRepository.AddExpense(entity);

            //return result ? 
            //    Result.Ok() :
            //    Result.Failure("Failed to add new expense.", ErrorCode.DATABASE_ERROR);
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

        private async Task<CustomActionResult<User>> GetUserAsync(string userEmail)
        {
            throw new NotImplementedException();

            //var user = await _userService.GetUserByEmailAsync(userEmail);

            //if (!user.Success || user.Value == null)
            //{
            //    return Result.Failure<User>(user.Error, user.ErrorCode);
            //}

            //return Result.Ok(user.Value);
        }
    }
}
