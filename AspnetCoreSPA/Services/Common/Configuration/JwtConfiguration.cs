namespace AspnetCoreSPATemplate.Services.Common.Configuration
{
    public class JwtConfiguration
    {
        public string Key { get; set; }

        public double DaysToExpiration { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}