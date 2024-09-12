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
                .ReturnsAsync(UserError.FailedToCreate);
            return this;
        }

        public IExpenseRepository Build() => repositoryMock.Object;
    }
}
