using System.ComponentModel.DataAnnotations;
// ReSharper disable CheckNamespace

namespace Services
{
    public class UserUpdateModel
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}