using Finance.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Finance.Domain.Dtos.Requests
{
    public class UserRequest
    {
        public UserRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User ToEntity(byte[] passwordHash, byte[] passwordSalt)
        {
            return new User(Name, Email, passwordHash, passwordSalt);
        }
    }
}