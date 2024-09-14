using Finance.Domain.Dtos.Requests;
using Finance.Domain.Errors;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
using Finance.Test.Mocks.Fixtures;
using Finance.Test.Mocks.Repositories;
using Finance.Test.Mocks.Services;
using Moq;
using System.Net;
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
                .AddSuccessfulCreateExpense()
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

        [Fact]
        public async Task CreateExpense_InvalidUser_ValidExpense_ShouldFail()
        {
            // Arrange
            var expense = ExpenseFixture.ValidExpenseRequest();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddSuccessfulCreateExpense()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(null)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.AddExpenseAsync("test", expense);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(ExpenseError.InvalidUser.StatusCode, result.StatusCode);
            Assert.Equal(ExpenseError.InvalidUser.Code, result.GetError().Code);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.AddExpenseAsync(It.IsAny<Expense>()), Times.Never);
        }

        [Fact]
        public async Task CreateExpense_ValidUser_ValidExpense_DbError_ShouldFail()
        {
            // Arrange
            var user = UserFixture.ValidUsers(1).FirstOrDefault();
            var expense = ExpenseFixture.ValidExpenseRequest();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddUnsuccessfulCreateExpense()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(user)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.AddExpenseAsync(user!.Email, expense);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(ExpenseError.FailedToCreate.StatusCode, result.StatusCode);
            Assert.Equal(ExpenseError.FailedToCreate.Code, result.GetError().Code);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(user.Email), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.AddExpenseAsync(It.IsAny<Expense>()), Times.Once);
        }

        [Fact]
        public async Task GetFilteredExpenses_ValidUser_ValidExpenses_ShouldSucceed()
        {
            // Arrange
            var userWithExpenses = UserFixture.ValidUsersWithExpenses(1, 5).FirstOrDefault();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddGetExpenses(userWithExpenses!.Expenses)
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(userWithExpenses!)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.GetFilteredExpensesAsync(userWithExpenses!.Email, new ExpensesFilterRequest());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(5, result.GetValue().Count);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(userWithExpenses!.Email), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.GetFilteredExpensesAsync(userWithExpenses!.Uid, It.IsAny<ExpensesFilterRequest>()), Times.Once);
        }

        [Fact]
        public async Task GetFilteredExpenses_InvalidUser_ShouldFail()
        {
            // Arrange
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddGetExpenses(null)
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(null)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.GetFilteredExpensesAsync("test", new ExpensesFilterRequest());

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(ExpenseError.InvalidUser.StatusCode, result.StatusCode);
            Assert.Equal(ExpenseError.InvalidUser.Code, result.GetError().Code);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.GetFilteredExpensesAsync(It.IsAny<Guid>(), It.IsAny<ExpensesFilterRequest>()), Times.Never);
        }

        [Fact]
        public async Task GetFilteredExpenses_ValidUser_EmptyExpenses_ShouldSucceed()
        {
            // Arrange
            var userWithExpenses = UserFixture.ValidUsersWithExpenses(1, 0).FirstOrDefault();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddGetExpenses(userWithExpenses!.Expenses)
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(userWithExpenses!)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.GetFilteredExpensesAsync(userWithExpenses!.Email, new ExpensesFilterRequest());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
            Assert.Empty(result.GetValue());

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(userWithExpenses!.Email), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.GetFilteredExpensesAsync(userWithExpenses!.Uid, It.IsAny<ExpensesFilterRequest>()), Times.Once);
        }

        [Fact]
        public async Task GetExpense_ValidUser_ValidExpense_ShouldSucceed()
        {
            // Arrange
            var userWithExpenses = UserFixture.ValidUsersWithExpenses(1, 5).FirstOrDefault();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddGetExpense(userWithExpenses!.Expenses.FirstOrDefault())
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(userWithExpenses!)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.GetExpenseAsync(userWithExpenses!.Email, userWithExpenses!.Expenses.FirstOrDefault()!.Uid.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(userWithExpenses.Expenses.FirstOrDefault()!.Uid, result.GetValue().Uid);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(userWithExpenses!.Email), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.GetExpenseAsync(userWithExpenses!.Expenses.FirstOrDefault()!.Uid), Times.Once);
        }

        [Fact]
        public async Task GetExpense_ValidUser_NullExpense_ShouldSucceed()
        {
            // Arrange
            var userWithExpenses = UserFixture.ValidUsersWithExpenses(1, 0).FirstOrDefault();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddGetExpense(null)
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(userWithExpenses!)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.GetExpenseAsync(userWithExpenses!.Email, Guid.NewGuid().ToString());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
            Assert.Null(result.Value);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(userWithExpenses!.Email), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.GetExpenseAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetExpense_InvalidUser_ShouldFail()
        {
            // Arrange
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(null)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.GetExpenseAsync("test", Guid.NewGuid().ToString());

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(ExpenseError.InvalidUser.StatusCode, result.StatusCode);
            Assert.Equal(ExpenseError.InvalidUser.Code, result.GetError().Code);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.GetExpenseAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DeleteExpense_InvalidUser_ShouldFail()
        {
            // Arrange
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(null)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.DeleteExpenseAsync("test", Guid.NewGuid().ToString());

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(ExpenseError.InvalidUser.StatusCode, result.StatusCode);
            Assert.Equal(ExpenseError.InvalidUser.Code, result.GetError().Code);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.DeleteExpenseAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DeleteExpense_ValidUser_ExpenseNotFound_ShouldFail()
        {
            // Arrange
            var user = UserFixture.ValidUsers(1).First();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddNotFoundDeleteExpense()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(user)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.DeleteExpenseAsync(user.Email, Guid.NewGuid().ToString());

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(ExpenseError.NotFound.StatusCode, result.StatusCode);
            Assert.Equal(ExpenseError.NotFound.Code, result.GetError().Code);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.DeleteExpenseAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteExpense_ValidUser_DbError_ShouldFail()
        {
            // Arrange
            var user = UserFixture.ValidUsers(1).First();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddUnsuccessfulDeleteExpense()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(user)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.DeleteExpenseAsync(user.Email, Guid.NewGuid().ToString());

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(ExpenseError.FailedToDelete.StatusCode, result.StatusCode);
            Assert.Equal(ExpenseError.FailedToDelete.Code, result.GetError().Code);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.DeleteExpenseAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteExpense_ValidUser_ExpenseDeleted_ShouldSucceed()
        {
            // Arrange
            var user = UserFixture.ValidUsers(1).First();
            var expenseRepository = new ExpenseRepositoryMockBuilder()
                .AddSuccessfulDeleteExpense()
                .Build();
            var userRepository = new UserRepositoryMockBuilder()
                .AddGetUserByEmail(user)
                .Build();

            var service = ExpenseServiceMock.Mock(userRepository, expenseRepository);

            // Act
            var result = await service.DeleteExpenseAsync(user.Email, Guid.NewGuid().ToString());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);

            Mock.Get(userRepository).Verify(r => r.GetUserByEmailAsync(It.IsAny<string>()), Times.Once);
            Mock.Get(expenseRepository).Verify(r => r.DeleteExpenseAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}