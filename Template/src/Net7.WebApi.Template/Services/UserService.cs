using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Net7.WebApi.Template.DataAccess.Contracts;
using Net7.WebApi.Template.Models;
using Net7.WebApi.Template.Models.Options;
using Net7.WebApi.Template.Resources.Users;

namespace Net7.WebApi.Template.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _uow;
        private readonly ConfigurationOptions _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(IUnitOfWork uow,
            IOptions<ConfigurationOptions> configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _uow = uow;
            _configuration = configuration.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Creates application user
        /// </summary>
        /// <param name="createUserRequest"></param>
        /// <returns></returns>
        public async Task<CreateUserResponse> Create(CreateUserRequest createUserRequest)
        {
            var appUser = createUserRequest.Adapt<ApplicationUser>();
            _uow.ApplicationUserRepository.Create(appUser);

            await _uow.SaveAsync();

            return appUser.Adapt<CreateUserResponse>();
        }

        /// <summary>
        /// Changes password
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task SetPasswordAsync([FromBody] ChangePasswordRequest req, Guid userId)
        {
            var applicationUser = _uow.ApplicationUserRepository.GetApplicationUser(userId);

            await _userManager.AddPasswordAsync(applicationUser, req.NewPassword);
        }
    }
}