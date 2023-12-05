using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Net7.WebApi.Template.DataAccess.Contracts;
using Net7.WebApi.Template.Exceptions;
using Net7.WebApi.Template.Models;
using Net7.WebApi.Template.Models.Options;
using Net7.WebApi.Template.Resources.Users;

namespace Net7.WebApi.Template.Services
{
    /// <inheritdoc/>
    public class UserService
    {
        private readonly IUnitOfWork _uow;
        private readonly ConfigurationOptions _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        /// <inheritdoc/>
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
            var res = await _userManager.CreateAsync(appUser);

            return appUser.Adapt<CreateUserResponse>();
        }

        /// <summary>
        /// Sets initial password
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task SetPasswordAsync([FromBody] ChangePasswordRequest req, Guid userId)
        {
            var applicationUser = _uow.ApplicationUserRepository.GetApplicationUser(userId);
            var res = await _userManager.AddPasswordAsync(applicationUser, req.NewPassword);

            if (!res.Succeeded)
            {
                throw new BadRequestException("Invalid password change attempt");
            }
        }

        //TODO: Change this endpoint,
        //remove userId parameter
        //when you implement login feature
        //and take userId from claims principal

        /// <summary>
        /// Changes already existing password
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public async Task ChangePasswordAsync([FromBody] ChangePasswordRequest req, Guid userId)
        {
            var applicationUser = _uow.ApplicationUserRepository.GetApplicationUser(userId);

            var res = await _userManager.ChangePasswordAsync(applicationUser, req.CurrentPassword, req.NewPassword);

            if (!res.Succeeded)
            {
                throw new BadRequestException("Invalid password change attempt");
            }
        }
    }
}