using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Identity
{
    public partial class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? CreatedTime { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}