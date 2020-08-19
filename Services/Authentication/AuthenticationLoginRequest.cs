using Microsoft.AspNetCore.Mvc;
// ReSharper disable CheckNamespace

namespace Services
{
    [BindProperties(SupportsGet = true)]
    public class AuthenticationLoginRequest : RequestBase
    {
        public AuthenticationLoginRequest()
            : base()
        {
        }

        public string CaptchaId { get; set; }

        public string UserEnteredCaptchaCode { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}