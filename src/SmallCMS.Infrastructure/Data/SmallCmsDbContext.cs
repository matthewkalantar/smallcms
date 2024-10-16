using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmallCMS.Domain.Entities;
using System.Reflection;

namespace SmallCMS.Infrastructure.Data
{
    public class SmallCmsDbContext : IdentityDbContext, ISmallCmsDbContext
    {
        public SmallCmsDbContext(DbContextOptions<SmallCmsDbContext> options) : base(options) { }
        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Default Data
            // TODO: Move to an extension method
            var hasher = new PasswordHasher<IdentityUser>();
            var user = new IdentityUser
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@site.com",
                NormalizedEmail = "ADMIN@SITE.COM",
                EmailConfirmed = true
            };
            user.PasswordHash = hasher.HashPassword(user, "q123456");
            builder.Entity<IdentityUser>().HasData(user);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
