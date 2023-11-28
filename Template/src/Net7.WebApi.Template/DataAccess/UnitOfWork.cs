using Net7.WebApi.Template.DataAccess.Contracts;
using Net7.WebApi.Template.DataAcess;

namespace Net7.WebApi.Template.DataAccess
{
    ///<inheritdoc/>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Parametrized constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        ///<inheritdoc/>
        public void Save()
        {
            _context.SaveChanges();
        }

        ///<inheritdoc/>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public void AutoDetectChangesEnabled(bool enable)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = enable;
        }

        ///<inheritdoc/>
        public void ClearContext()
        {
            _context.ChangeTracker.Clear();
        }
    }
}