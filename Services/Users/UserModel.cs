using System.ComponentModel.DataAnnotations;
// ReSharper disable CheckNamespace

namespace Services
{
    public class UserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}