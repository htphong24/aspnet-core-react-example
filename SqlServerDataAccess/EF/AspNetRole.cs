using System.Collections.Generic;

namespace SqlServerDataAccess.EF
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}