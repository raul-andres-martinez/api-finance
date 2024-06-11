namespace Finance.Domain.Models
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

        public List<Expense> Expenses { get; private set;} = [];
    }
}
