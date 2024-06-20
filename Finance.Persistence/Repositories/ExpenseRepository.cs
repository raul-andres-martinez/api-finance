using Finance.Domain.Dtos.Requests;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
using Finance.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Expense>> GetFilteredExpensesAsync(string userEmail, ExpensesFilterRequest request)
        {
            var query = _context.Expenses
                                .Include(e => e.User)
                                .AsQueryable();

            query = query.Where(e => e.User != null && e.User.Email == userEmail);

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(e => e.Category == request.Category);
            }

            if (request.PaymentMethod.HasValue)
            {
                query = query.Where(e => e.PaymentMethod == request.PaymentMethod.Value);
            }

            if (request.Currency.HasValue)
            {
                query = query.Where(e => e.Currency == request.Currency.Value);
            }

            if (request.Recurring.HasValue)
            {
                query = query.Where(e => e.Recurring == request.Recurring.Value);
            }

            return await query.ToListAsync();
        }

    }
}
