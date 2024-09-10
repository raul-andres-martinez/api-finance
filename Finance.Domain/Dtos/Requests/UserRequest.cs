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

        public static implicit operator User(UserRequest request)
        {
            return new User(request.Name, request.Email, Array.Empty<byte>(), Array.Empty<byte>());
        }
    }
}