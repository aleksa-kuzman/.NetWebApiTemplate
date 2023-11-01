namespace Net7.WebApi.Template.Models.Options
{
    public class TokenOptions
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public int ExpirationMinutes { get; set; }
        public bool RequireHttps { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
    }
}