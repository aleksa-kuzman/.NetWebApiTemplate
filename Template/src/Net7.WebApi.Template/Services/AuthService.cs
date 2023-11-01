using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Net7.WebApi.Template.Models;
using Net7.WebApi.Template.Models.Options;
using Net7.WebApi.Template.Resources.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Net7.WebApi.Template.Services
{
    /// <summary>
    /// Service that implements authorization and authentication logic
    /// </summary>
    public class AuthService
    {
        private readonly ILogger _logger;
        private readonly ConfigurationOptions _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        ///<inheritdoc/>
        public AuthService(
            ILogger<AuthService> logger,
            IOptions<ConfigurationOptions> configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _configuration = configuration.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new Exception();
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user!.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            user.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddMinutes(_configuration.Tokens.RefreshTokenExpirationMinutes);
            //await _uow.SaveAsync();

            var accessTokenExpiresInMinutes = _configuration.Tokens.ExpirationMinutes * 60;

            var loginResponse = new LoginResponse
            {
                AccessToken = CreateToken(authClaims),
                Email = request.Email,
                RefreshToken = user.RefreshToken,
                AccessTokenExpiresIn = accessTokenExpiresInMinutes
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            return loginResponse;
        }

        private string CreateToken(List<Claim> authClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Tokens.Key));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(_configuration.Tokens.ExpirationMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return tokenHandler.WriteToken(token);
        }
    }
}