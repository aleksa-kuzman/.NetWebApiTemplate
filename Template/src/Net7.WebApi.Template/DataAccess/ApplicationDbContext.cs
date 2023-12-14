using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Net7.WebApi.Template.DataAccess;
using Net7.WebApi.Template.Models;
using Net7.WebApi.Template.Models.Options;

namespace Net7.WebApi.Template.DataAcess
{
    ///<inheritdoc/>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IDataProtectionKeyContext
    {
        private readonly ConfigurationOptions _configuration;

        ///<inheritdoc/>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<ConfigurationOptions> configuration) : base(options)
        {
            _configuration = configuration.Value;
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
            builder.SeedRoles();
            builder.SeedAdmin(_configuration.AdminPassword);
            base.OnModelCreating(builder);
        }
    }
}