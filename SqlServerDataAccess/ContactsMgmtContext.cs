using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SqlServerDataAccess.Entities;

namespace SqlServerDataAccess
{
    public class ContactsMgmtContext : DbContext
    {
        public ContactsMgmtContext(DbContextOptions<ContactsMgmtContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
