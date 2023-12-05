using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net7.WebApi.Template.Resources;
using Net7.WebApi.Template.Resources.Users;
using Net7.WebApi.Template.Services;

namespace Net7.WebApi.Template.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), 200)]
        [ProducesResponseType(typeof(HttpErrorResponse), 400)]
        [ProducesResponseType(typeof(HttpErrorResponse), 401)]
        [ProducesResponseType(typeof(HttpErrorResponse), 403)]
        [ProducesResponseType(typeof(HttpErrorResponse), 500)]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest req)
        {
            return Ok(await _userService.Create(req));
        }

        /// <summary>
        /// Adds password to a newly created user
        /// </summary>
        /// <returns></returns>
        [HttpPost("SetPassword")]
        [ProducesResponseType(typeof(CreateUserResponse), 200)]
        [ProducesResponseType(typeof(HttpErrorResponse), 400)]
        [ProducesResponseType(typeof(HttpErrorResponse), 401)]
        [ProducesResponseType(typeof(HttpErrorResponse), 403)]
        [ProducesResponseType(typeof(HttpErrorResponse), 500)]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<ActionResult> AddPassword([FromBody] ChangePasswordRequest req, Guid userId)
        {
            await _userService.SetPasswordAsync(req, userId);
            return Ok();
        }

        /// <summary>
        /// Admin resets an already existing password
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [ProducesResponseType(typeof(CreateUserResponse), 200)]
        [ProducesResponseType(typeof(HttpErrorResponse), 400)]
        [ProducesResponseType(typeof(HttpErrorResponse), 401)]
        [ProducesResponseType(typeof(HttpErrorResponse), 403)]
        [ProducesResponseType(typeof(HttpErrorResponse), 500)]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest req, Guid userId)
        {
            await _userService.ChangePasswordAsync(req, userId);
            return Ok();
        }
    }
}