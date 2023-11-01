using Microsoft.AspNetCore.DataProtection;
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
        private readonly IDataProtector _protector;

        ///<inheritdoc/>
        public AuthService(
            ILogger<AuthService> logger,
            IOptions<ConfigurationOptions> configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDataProtectionProvider dataProtectionProvider)
        {
            _logger = logger;
            _configuration = configuration.Value;
            _userManager = userManager;
            _roleManager = roleManager;
            _protector = dataProtectionProvider.CreateProtector("Net7.WebApi.Template.Services.AuthService.RefreshToken.V1");
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

            var accessTokenExpiresInMinutes = _configuration.Tokens.ExpirationMinutes * 60;

            var refreshToken = CreateRefreshToken(user);

            var loginResponse = new LoginResponse
            {
                AccessToken = CreateAccessToken(authClaims),
                Email = request.Email,
                RefreshToken = refreshToken,
                AccessTokenExpiresIn = accessTokenExpiresInMinutes
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //await _uow.SaveAsync();

            return loginResponse;
        }

        private string CreateRefreshToken(ApplicationUser user)
        {
            var refreshToken = user.RefreshToken;

            if (string.IsNullOrEmpty(refreshToken))
            {
                refreshToken = $"{Guid.NewGuid().ToString()}-{Guid.NewGuid().ToString()}";
                refreshToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(refreshToken));
                user.RefreshToken = _protector.Protect(refreshToken);
            }
            else
            {
                refreshToken = _protector.Unprotect(refreshToken);
            }

            return refreshToken;
        }

        private string CreateAccessToken(List<Claim> authClaims)
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