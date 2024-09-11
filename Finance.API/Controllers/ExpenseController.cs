using Finance.Domain.Dtos.Requests;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Utils.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finance.API.Controllers
{
    [Route("/api/v1/expenses")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<CustomActionResult> AddExpense(ExpenseRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _expenseService.AddExpenseAsync(userEmail, request);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetExpenses([FromQuery] ExpensesFilterRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _expenseService.GetFilteredExpensesAsync(userEmail, request);
        }
    }
}