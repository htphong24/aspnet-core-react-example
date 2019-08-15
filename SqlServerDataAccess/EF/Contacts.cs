using System;
using System.Collections.Generic;

namespace SqlServerDataAccess.EF
{
    public partial class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Post { get; set; }

        public string Email { get; set; }

        public string Web { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }
    }
}
