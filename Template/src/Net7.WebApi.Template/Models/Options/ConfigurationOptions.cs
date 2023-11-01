namespace Net7.WebApi.Template.Models.Options
{
    /// <summary>
    /// IOptions object for app config portion of
    /// </summary>
    public class ConfigurationOptions
    {
        public const string ConfigKey = "Net7.WebApi.Template_Config";
        public string AppName { get; set; } = null!;

        public TokenOptions Tokens { get; set; }
        public DataProtectionOptions DataProtection { get; set; } = null!;
    }
}