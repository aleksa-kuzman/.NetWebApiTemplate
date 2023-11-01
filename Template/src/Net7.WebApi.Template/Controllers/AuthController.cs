using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net7.WebApi.Template.Resources;
using Net7.WebApi.Template.Resources.Auth;

namespace Net7.WebApi.Template.Controllers
{
    /// <summary>
    /// Handles authentication and authorization
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/helloworld")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(HttpErrorResponse), 400)]
        [ProducesResponseType(typeof(HttpErrorResponse), 401)]
        [ProducesResponseType(typeof(HttpErrorResponse), 403)]
        [ProducesResponseType(typeof(HttpErrorResponse), 500)]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            throw new NotImplementedException();
        }
    }
}