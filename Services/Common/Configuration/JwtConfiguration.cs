// ReSharper disable CheckNamespace
namespace Services
{
    public class JwtConfiguration
    {
        public string Key { get; set; }

        public double DaysToExpiration { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}