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

        public ExpenseRepositoryMockBuilder AddSuccessfulCreateExpense()
        {
            repositoryMock.Setup(repo => repo.AddExpenseAsync(It.IsAny<Expense>()))
                .ReturnsAsync(CustomActionResult.Created);
            return this;
        }

        public ExpenseRepositoryMockBuilder AddUnsuccessfulCreateExpense()
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
                .ReturnsAsync((Guid id) => expense);

            return this;
        }

        public ExpenseRepositoryMockBuilder AddNotFoundDeleteExpense()
        {
            repositoryMock.Setup(repo => repo.DeleteExpenseAsync(It.IsAny<Guid>()))
                .ReturnsAsync(ExpenseError.NotFound);

            return this;
        }

        public ExpenseRepositoryMockBuilder AddUnsuccessfulDeleteExpense()
        {
            repositoryMock.Setup(repo => repo.DeleteExpenseAsync(It.IsAny<Guid>()))
                .ReturnsAsync(ExpenseError.FailedToDelete);

            return this;
        }

        public ExpenseRepositoryMockBuilder AddSuccessfulDeleteExpense()
        {
            repositoryMock.Setup(repo => repo.DeleteExpenseAsync(It.IsAny<Guid>()))
                .ReturnsAsync(CustomActionResult.NoContent());

            return this;
        }

        public IExpenseRepository Build() => repositoryMock.Object;
    }
}