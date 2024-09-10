﻿using Finance.Domain.Dtos;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Utils.Result;

namespace Finance.Domain.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<CustomActionResult> AddExpenseAsync(string? userEmail, ExpenseRequest request);
        Task<CustomActionResult<List<ExpenseResponse>>> GetFilteredExpensesAsync(string userEmail, ExpensesFilterRequest request);
    }
}