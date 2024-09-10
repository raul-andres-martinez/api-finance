using Bogus;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Enum;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils;

namespace Finance.Test.Mocks.Fixtures
{
    public static class UserFixture
    {
        public static List<User> ValidUsers(int quantity) => CreateValidUsers(quantity);
        public static List<User> ValidUsersWithExpenses(int userQuantity, int expenseQuantity) => CreateValidUsersWithExpenses(userQuantity, expenseQuantity);
        public static UserRequest ValidUserRequest => CreateValidUserRequest();
        public static LoginRequest ValidLoginRequest => CreateValidLoginRequest();
        public static (User user, LoginRequest login) JohnDoeLogin => CreateJohnDoeLoginRequest();
        public static User JohnDoe => CreateJohnDoe();

        private static List<User> CreateValidUsers(int quantity)
        {
            var userFaker = new Faker<User>()
                .CustomInstantiator(faker =>
                    new User(
                        faker.Person.FirstName,
                        faker.Person.Email,
                        faker.Random.Bytes(64),
                        faker.Random.Bytes(64)));

            return userFaker.Generate(quantity);
        }

        private static List<User> CreateValidUsersWithExpenses(int userQuantity, int expenseQuantity)
        {
            var users = CreateValidUsers(userQuantity);

            foreach(var user in users)
            {
                for(var i = 0; i < expenseQuantity; i++)
                {
                    user.AddExpense(CreateValidExpense(user.Uid));
                }
            }

            return users;
        }

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

        private static UserRequest CreateValidUserRequest() 
        {
            var userRequestFaker = new Faker<UserRequest>()
                .CustomInstantiator(faker =>
                    new UserRequest(
                        faker.Person.FirstName,
                        faker.Person.Email,
                        faker.Random.String(20)));

            return userRequestFaker.Generate();
        }

        private static LoginRequest CreateValidLoginRequest()
        {
            var loginRequestFaker = new Faker<LoginRequest>()
                .CustomInstantiator(faker =>
                    new LoginRequest(
                        faker.Person.Email,
                        faker.Random.String(20)));

            return loginRequestFaker.Generate();
        }

        private static (User user, LoginRequest login) CreateJohnDoeLoginRequest()
        {
            var email = "johndoe@email.com";
            var password = "hashstring";

            PasswordHasher.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User("John Doe", email, passwordSalt, passwordHash);

            var loginRequest = new LoginRequest(email, password);

            return (user, loginRequest);
        }

        private static User CreateJohnDoe()
        {
            var salt = Convert.FromBase64String("c2FsdHRlc3Q=");
            var hash = Convert.FromBase64String("aGFzaHN0cmluZw==");

            return new User("John Doe", "johndoe@email.com", salt, hash);
        }
    }
}