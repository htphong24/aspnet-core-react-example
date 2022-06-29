using System;
using System.Collections.Generic;

namespace SqlServerDataAccess.EF
{
    public partial class AspNetUser
    {
        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }

        //public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
        public ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new HashSet<AspNetUserClaim>();
        public ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new HashSet<AspNetUserLogin>();
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new HashSet<AspNetUserRole>();
    }
}