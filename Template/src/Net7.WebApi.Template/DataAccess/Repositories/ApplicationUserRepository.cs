using Net7.WebApi.Template.DataAccess.Contracts;
using Net7.WebApi.Template.DataAcess;

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
            ILogger<ApplicationUserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}