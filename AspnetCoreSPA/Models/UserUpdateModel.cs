using System.ComponentModel.DataAnnotations;

namespace AspnetCoreSPATemplate.Models
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