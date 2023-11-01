namespace Net7.WebApi.Template.Resources.Auth
{
    /// <summary>
    /// Format of response of /login endpoint
    /// </summary>
    public class LoginResponse
    {
        /// <inheritdoc />
        public string Email { get; set; } = null!;

        /// <inheritdoc />
        public string Name { get; set; } = null!;

        /// <inheritdoc />
        public string AccessToken { get; set; } = null!;

        /// <inheritdoc />
        public int AccessTokenExpiresIn { get; set; }

        /// <inheritdoc />
        public string RefreshToken { get; set; } = null!;

        /// <inheritdoc />
        public string UserRole { get; set; } = null!;
    }
}