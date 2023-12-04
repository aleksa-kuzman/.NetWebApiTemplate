using Net7.WebApi.Template.Models;

namespace Net7.WebApi.Template.DataAccess.Contracts
{
    /// <summary>
    /// Operations with user regarding the database
    /// </summary>
    public interface IApplicationUserRepository
    {
        /// <summary>
        /// Creating application user
        /// </summary>
        /// <param name="applicationUser"></param>
        void Create(ApplicationUser applicationUser);

        /// <summary>
        /// Get application user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApplicationUser GetApplicationUser(Guid id);
    }
}