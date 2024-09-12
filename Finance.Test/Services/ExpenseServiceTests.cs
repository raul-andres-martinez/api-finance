using Finance.Domain.Dtos.Requests;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
using Finance.Service.Services;
using Finance.Test.Mocks.Fixtures;
using Finance.Test.Mocks.Repositories;
using Finance.Test.Mocks.Services;
using Moq;
using Xunit;

namespace Finance.Test.Services
{
    public class ExpenseServiceTests
    {
        [Fact]
        public async Task CreateExpense_ValidUser_ValidExpense_ShouldSucceed()
        {
            // Arrange
            var user = UserFixture.ValidUsers(1).FirstOrDefault();
            var expense = ExpenseFixture.ValidExpenseRequest();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddSuccessfullCreateExpense()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(user)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.AddExpenseAsync(user!.Email, expense);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(CustomActionResult.Created().StatusCode, result.StatusCode);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(user.Email), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.AddExpenseAsync(It.IsAny<Expense>()), Times.Once);
        }
    }
}
