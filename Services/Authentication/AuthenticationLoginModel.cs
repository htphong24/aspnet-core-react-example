// ReSharper disable CheckNamespace

namespace Services
{
    public class AuthenticationLoginModel
    {
        public bool Successful { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }
    }
}