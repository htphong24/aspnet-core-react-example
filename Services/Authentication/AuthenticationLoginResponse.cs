// ReSharper disable CheckNamespace

namespace Services
{
    public class AuthenticationLoginResponse : ResponseBase
    {
        public AuthenticationLoginResponse()
            : base()
        {
        }

        public string AccessToken { get; set; }

        //public ApplicationUser User { get; set; }

        public bool CaptchaNeeded { get; set; }
    }
}