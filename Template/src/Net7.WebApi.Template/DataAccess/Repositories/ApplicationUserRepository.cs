using Net7.WebApi.Template.DataAccess.Contracts;
using Net7.WebApi.Template.DataAcess;
using Net7.WebApi.Template.Exceptions;
using Net7.WebApi.Template.Models;

namespace Net7.WebApi.Template.DataAccess.Repositories
{
    ///<inheritdoc/>
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Parametrized constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public ApplicationUserRepository(
            ApplicationDbContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Create(ApplicationUser applicationUser)
        {
            _context.ApplicationUsers.Add(applicationUser);
        }

        /// <inheritdoc/>
        public ApplicationUser GetApplicationUser(Guid id)
        {
            var applicationUser = _context.ApplicationUsers.Where(m => m.Id == id).FirstOrDefault();
            if (applicationUser == null)
            {
                throw new NotFoundException("User not found");
            }
            return applicationUser;
        }
    }
}