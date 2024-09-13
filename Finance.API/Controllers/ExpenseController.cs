using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<CustomActionResult> AddExpense(ExpenseRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _expenseService.AddExpenseAsync(userEmail, request);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<CustomActionResult<List<ExpenseResponse>>> GetExpenses([FromQuery] ExpensesFilterRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _expenseService.GetFilteredExpensesAsync(userEmail, request);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<ExpenseResponse> GetExpense([FromQuery] string id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _expenseService.GetExpenseAsync(userEmail, id);
        }
    }
}