using System.ComponentModel.DataAnnotations;

namespace AspnetCoreSPATemplate.Models
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