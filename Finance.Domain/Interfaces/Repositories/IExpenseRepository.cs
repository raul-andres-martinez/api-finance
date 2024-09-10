using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;

namespace Finance.Domain.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task<CustomActionResult> AddExpenseAsync(Expense expense);
    }
}