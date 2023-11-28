using Microsoft.AspNetCore.Mvc;
using Net7.WebApi.Template.Resources;
using Net7.WebApi.Template.Resources.Users;

namespace Net7.WebApi.Template.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), 200)]
        [ProducesResponseType(typeof(HttpErrorResponse), 400)]
        [ProducesResponseType(typeof(HttpErrorResponse), 401)]
        [ProducesResponseType(typeof(HttpErrorResponse), 403)]
        [ProducesResponseType(typeof(HttpErrorResponse), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest req)
        {
            throw new NotImplementedException();
            //return Ok(await _usersService.Create(req));
        }
    }
}