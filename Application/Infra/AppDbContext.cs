using Application.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Infra
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Talk> Talks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TalkToUser> TalksToUsers{get; set;}

        public DbSet<MessageTallkToUser> MessageTallkToUsers { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .Ignore(u => u.AccessFailedCount)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.TwoFactorEnabled)
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed);
            modelBuilder.Entity<Message>()
                .Property(m => m.File)
                .HasMaxLength(20 * 1024 * 1024);

            modelBuilder.Entity<Message>()
                .Property(m => m.Text)
                .HasMaxLength(2000);

        }
    }

}
