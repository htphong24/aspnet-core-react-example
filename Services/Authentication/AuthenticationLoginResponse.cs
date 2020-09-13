// ReSharper disable CheckNamespace

namespace Services
{
    public class AuthenticationLoginResponse : ResponseBase
    {
        public AuthenticationLoginResponse()
            : base()
        {
        }

        public AuthenticationLoginModel AuthLogin { get; set; }

        public bool CaptchaNeeded { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //public ApplicationUser User { get; set; }
    }
}