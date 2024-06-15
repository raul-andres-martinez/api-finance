using Finance.Domain.Dtos;

namespace Finance.Domain.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<bool> AddExpenseAsync(ExpenseRequest request);
    }
}
