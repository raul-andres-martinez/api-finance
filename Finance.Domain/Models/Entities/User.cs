namespace Finance.Domain.Models.Entities
{
    public class User : Entity
    {
        public User(string name, string email, byte[] passwordSalt, byte[] passwordHash)
        {
            Name = name;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public byte[] PasswordHash { get; private set; }

        public List<Expense> Expenses { get; private set; } = [];

        public void SetHashSalt(byte[] hash, byte[] salt)
        {
            PasswordHash = hash;
            PasswordSalt = salt;
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
        }
    }
}