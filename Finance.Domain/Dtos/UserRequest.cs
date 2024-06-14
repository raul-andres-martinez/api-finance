using Finance.Domain.Models.Entities;

namespace Finance.Domain.Dtos
{
    public class UserRequest
    {
        public UserRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get ; set; }
        public string Password { get; set; }

        public User ToEntity(byte[] passwordHash, byte[] passwordSalt)
        {
            return new User(Name, Email, passwordHash, passwordSalt);
        }
    }
}