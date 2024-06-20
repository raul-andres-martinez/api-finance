using Finance.Domain.Dtos.Requests;
using Finance.Domain.Models.Entities;

namespace Finance.Domain.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task<bool> AddExpense(Expense expense);
        Task<List<Expense>> GetFilteredExpensesAsync(string userEmail, ExpensesFilterRequest request);
    }
}
