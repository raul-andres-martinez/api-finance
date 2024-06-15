using Finance.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("/api/v1/expenses")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
    }
}
