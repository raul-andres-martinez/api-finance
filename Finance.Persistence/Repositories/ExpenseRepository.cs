using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
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

        public async Task<CustomActionResult> AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            var changes = await _context.SaveChangesAsync();

            if (changes is not 1)
            {
                return ExpenseError.FailedToCreate;
            }

            return CustomActionResult.Created();
        }
    }
}
