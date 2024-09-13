using Bogus;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Enum;
using Finance.Domain.Models.Entities;

namespace Finance.Test.Mocks.Fixtures
{
    public class ExpenseFixture
    {
        public static Expense ValidExpense(Guid userId) => CreateValidExpense(userId);
        public static ExpenseRequest ValidExpenseRequest() => CreateValidExpenseRequest();

        private static Expense CreateValidExpense(Guid userId)
        {
            var expenseFaker = new Faker<Expense>()
                .CustomInstantiator(faker =>
                    new Expense(
                        faker.Finance.Amount(),
                        faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now),
                        faker.Commerce.Categories(1)[0],
                        faker.Lorem.Sentence(10),
                        faker.PickRandom<PaymentMethod>(),
                        faker.PickRandom<Currency>(),
                        faker.Random.Bool() ? faker.Random.Int(1, 30) : (int?)null,
                        userId));

            return expenseFaker.Generate();
        }

        private static ExpenseRequest CreateValidExpenseRequest()
        {
            var expenseFaker = new Faker<ExpenseRequest>()
                .CustomInstantiator(faker =>
                    new ExpenseRequest(
                        faker.Finance.Amount(),
                        faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now),
                        faker.Commerce.Categories(1)[0],
                        faker.Lorem.Sentence(10),
                        faker.PickRandom<PaymentMethod>(),
                        faker.PickRandom<Currency>(),
                        faker.Random.Bool() ? faker.Random.Int(1, 30) : (int?)null));

            return expenseFaker.Generate();
        }
    }
}