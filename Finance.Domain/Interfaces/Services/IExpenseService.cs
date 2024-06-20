using Finance.Domain.Dtos;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;

namespace Finance.Domain.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<Result> AddExpenseAsync(string userEmail, ExpenseRequest request);
        Task<Result<List<ExpenseResponse>>> GetFilteredExpensesAsync(string userEmail, ExpensesFilterRequest request);
    }
}
