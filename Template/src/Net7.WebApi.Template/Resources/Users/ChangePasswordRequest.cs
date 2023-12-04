namespace Net7.WebApi.Template.Resources.Users
{
    /// <summary>
    /// Change password request DTO
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <inheritdoc/>
        public string CurrentPassword { get; set; } = null!;

        /// <inheritdoc/>
        public string NewPassword { get; set; } = null!;

        /// <inheritdoc/>
        public string VerifyPassword { get; set; } = null!;
    }
}