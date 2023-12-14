using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Net7.WebApi.Template.Models;

namespace Net7.WebApi.Template.DataAccess
{
    /// <summary>
    /// Used to make method that will configure
    /// db Context with fluent api
    /// and to seed data
    /// </summary>
    public static class ApplicationDbContextConfig
    {
        /// <summary>
        /// Inserts roles
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>()
                .HasData(
                new ApplicationRole { Id = Guid.Parse("5a2e0382-92ae-4e1e-89e2-7b0454c45355"), Name = "Admin", NormalizedName = "Admin".Normalize() },
                new ApplicationRole { Id = Guid.Parse("550a0821-5fcf-41f3-8ace-0b698e70a6a9"), Name = "RegularUser", NormalizedName = "RegularUser".Normalize() }
                );
        }

        /// <summary>
        /// Seeds initial administrator account
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="adminPassword"></param>
        public static void SeedAdmin(this ModelBuilder modelBuilder, string adminPassword)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var appUser = new ApplicationUser { Id = Guid.Parse("531eee9e-ee49-4ff1-a3c8-728af795cb75"), UserName = "System Admin", Email = "aleksa.kuzman.996@gmail.com", NormalizedEmail = "ALEKSA.KUZMAN.996@GMAIL.COM" };
            appUser.PasswordHash = passwordHasher.HashPassword(appUser, adminPassword);

            modelBuilder.Entity<ApplicationUser>()
                .HasData(appUser);

            modelBuilder.Entity<IdentityUserRole<Guid>>()
                .HasData(

                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("5a2e0382-92ae-4e1e-89e2-7b0454c45355"),
                    UserId = Guid.Parse("531eee9e-ee49-4ff1-a3c8-728af795cb75")
                });
        }
    }
}