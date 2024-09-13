using Finance.Domain.Dtos.Requests;
using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
using Moq;

namespace Finance.Test.Mocks.Repositories
{
    public class ExpenseRepositoryMockBuilder
    {
        private readonly Mock<IExpenseRepository> repositoryMock = new();

        public ExpenseRepositoryMockBuilder AddSuccessfullCreateExpense()
        {
            repositoryMock.Setup(repo => repo.AddExpenseAsync(It.IsAny<Expense>()))
                .ReturnsAsync(CustomActionResult.Created);
            return this;
        }

        public ExpenseRepositoryMockBuilder AddUnsuccessfullCreateExpense()
        {
            repositoryMock.Setup(repo => repo.AddExpenseAsync(It.IsAny<Expense>()))
                .ReturnsAsync(ExpenseError.FailedToCreate);
            return this;
        }

        public ExpenseRepositoryMockBuilder AddGetExpenses(List<Expense>? expenses)
        {
            repositoryMock.Setup(repo => repo.GetFilteredExpensesAsync(It.IsAny<Guid>(), It.IsAny<ExpensesFilterRequest>()))
                .ReturnsAsync((Guid userId, ExpensesFilterRequest request) =>
                    expenses != null ? expenses : []
                );

            return this;
        }

        public ExpenseRepositoryMockBuilder AddGetExpense(Expense? expense)
        {
            repositoryMock.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid userId) => expense);

            return this;
        }

        public IExpenseRepository Build() => repositoryMock.Object;
    }
}