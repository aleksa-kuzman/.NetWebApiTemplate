namespace Net7.WebApi.Template.Models.Options
{
    /// <summary>
    /// IOptions object for app config portion of
    /// </summary>
    public class ConfigurationOptions
    {
        /// <summary>
        /// The name of key where configuration options are  stored in
        /// appsettings.json file
        /// </summary>
        public const string ConfigKey = "Net7.WebApi.Template_Config";

        /// <summary>
        /// The name of this application
        /// </summary>
        public string AppName { get; set; } = null!;

        /// <summary>
        /// Password for seeded administrator user
        /// </summary>
        public string AdminPassword { get; set; } = null!;

        /// <summary>
        /// Configuration for bearer and refresh tokens
        /// </summary>
        public TokenOptions Tokens { get; set; } = null!;

        /// <summary>
        /// Options for cryptography
        /// </summary>
        public DataProtectionOptions DataProtection { get; set; } = null!;
    }
}