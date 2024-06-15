using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
using Finance.Persistence.Context;

namespace Finance.Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly FinanceContext _context;

        public ExpenseRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task<bool> AddExpense(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
