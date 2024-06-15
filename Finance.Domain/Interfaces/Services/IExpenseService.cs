using Finance.Domain.Dtos;
using Finance.Domain.Dtos.Requests;

namespace Finance.Domain.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<Result> AddExpenseAsync(string userEmail, ExpenseRequest request);
    }
}
