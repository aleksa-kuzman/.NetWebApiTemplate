namespace Net7.WebApi.Template.Models.Options
{
    public class DataProtectionOptions
    {
        public string PfxBase64 { get; set; } = null!;
        public string PfxPwd { get; set; } = null!;
        public int IterCount { get; set; }
    }
}