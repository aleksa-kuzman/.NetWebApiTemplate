using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net7.WebApi.Template.Models;

namespace Net7.WebApi.Template.DataAcess
{
    ///<inheritdoc/>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IDataProtectionKeyContext
    {
        ///<inheritdoc/>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        ///<inheritdoc/>
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

        ///<inheritdoc/>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        ///<inheritdoc/>
        public DbSet<ApplicationRole> ApplicationRoles { get; set; } = null!;

        ///<inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}