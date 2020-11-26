using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sppo.Areas.Identity.Data;
using SPPO.EntityModels;

namespace sppo.Data
{
    public class MyContext : IdentityDbContext<Profile>
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountGrade>()
                .HasKey(p => new { p.RecieverID, p.GiversID });

            modelBuilder.Entity<LogUserVisited>()
               .HasKey(p => new { p.LogId, p.UserId });
        }
        public DbSet<User> users { get; set; }
        public DbSet<Profile> profiles { get; set; }
        public DbSet<AccountGrade> accountGrades { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Edit> edits { get; set; }
        public DbSet<Language> languages { get; set; }
        public DbSet<Theme> themes { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<Form> forms { get; set; }
        public DbSet<Statistic> statistics { get; set; }
        public DbSet<LoyaltyPoints> loyaltyPoints { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Advertisement> advertisements { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<CreditCard> creditCards { get; set; }
        public DbSet<Gender> genders { get; set; }
        public DbSet<Log> logs { get; set; }
        public DbSet<LogUserVisited> logUserVisiteds { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Transaction> transactions { get; set; }
    }
}
