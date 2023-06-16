namespace Contacts.Data
{
    using Contacts.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ContactsDbContext : IdentityDbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public virtual DbSet<Contact> Contacts { get; set; } = null!;

        public virtual DbSet<ApplicationUserContact> ApplicationUsersContacts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserContact>()
                .HasKey(pk => new { pk.ApplicationUserId, pk.ContactId });

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.ApplicationUsersContacts)
                .WithOne(u => u.ApplicationUser)
                    .HasForeignKey(u => u.ApplicationUserId);

            builder.Entity<Contact>()
                .HasMany(u => u.ApplicationUsersContacts)
                .WithOne(u => u.Contact)
                    .HasForeignKey(u => u.ContactId);

            builder
               .Entity<Contact>()
               .HasData(new Contact()
               {
                   Id = 1,
                   FirstName = "Bruce",
                   LastName = "Wayne",
                   PhoneNumber = "+359881223344",
                   Address = "Gotham City",
                   Email = "imbatman@batman.com",
                   Website = "www.batman.com"
               });

            base.OnModelCreating(builder);
        }
    }
}