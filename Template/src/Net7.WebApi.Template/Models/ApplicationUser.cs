using Microsoft.AspNetCore.Identity;

namespace Net7.WebApi.Template.Models
{
    /// <inheritdoc />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Stores refresh token
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Stores the date afther which refresh token is no longer viable
        /// </summary>
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }
    }
}