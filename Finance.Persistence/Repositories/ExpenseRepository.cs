using Finance.Domain.Dtos.Requests;
using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
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

        public async Task<CustomActionResult<List<Expense>>> GetFilteredExpensesAsync(Guid userId, ExpensesFilterRequest request)
        {
            var query = _context.Expenses.Where(
                x => x.UserId == userId);

            if (!string.IsNullOrEmpty(request.Category))
            {
                query.Where(x => x.Category == request.Category);
            }

            if (request.PaymentMethod.HasValue)
            {
                query.Where(x => x.PaymentMethod == request.PaymentMethod.Value);
            }

            if (request.Currency.HasValue)
            {
                query.Where(x => x.Currency == request.Currency.Value);
            }

            if (request.Recurring.HasValue)
            {
                query.Where(x => x.Recurring == request.Recurring.Value);
            }

            return await query.ToListAsync();
        }
    }
}