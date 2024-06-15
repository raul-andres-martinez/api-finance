using Finance.Domain.Models.Entities;

namespace Finance.Domain.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task<bool> AddExpense(Expense expense);
    }
}
