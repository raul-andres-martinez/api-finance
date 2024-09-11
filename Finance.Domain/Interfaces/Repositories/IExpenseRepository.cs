using Finance.Domain.Dtos.Requests;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;

namespace Finance.Domain.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task<CustomActionResult> AddExpenseAsync(Expense expense);
        Task<CustomActionResult<List<Expense>>> GetFilteredExpensesAsync(Guid userId, ExpensesFilterRequest request);
    }
}