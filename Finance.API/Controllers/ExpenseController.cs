using Finance.Domain.Dtos.Requests;
using Finance.Domain.Interfaces.Services;
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
        public async Task<IActionResult> AddExpense(ExpenseRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var response = await _expenseService.AddExpenseAsync(userEmail, request);

            if (!response.Success)
            {
                return BadRequest(response.Error);
            }

            return Ok();
        }
    }
}
