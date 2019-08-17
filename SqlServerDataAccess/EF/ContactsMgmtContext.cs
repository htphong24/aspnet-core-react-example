using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SqlServerDataAccess.EF
{
    public partial class ContactsMgmtContext : DbContext
    {
        public ContactsMgmtContext()
        {
        }

        public ContactsMgmtContext(DbContextOptions<ContactsMgmtContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Company).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone1)
                    .IsRequired()
                    .HasMaxLength(127);

                entity.Property(e => e.Phone2).HasMaxLength(127);

                entity.Property(e => e.Post).HasMaxLength(255);

                entity.Property(e => e.State).HasMaxLength(255);

                entity.Property(e => e.Web).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(127);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(127);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(127);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(127);

                entity.Property(e => e.UpdatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(127);
            });
        }
    }
}
