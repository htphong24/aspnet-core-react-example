// ReSharper disable CheckNamespace

namespace Services
{
    public class AuthenticationLoginGetResponse : ResponseBase
    {
        public AuthenticationLoginGetResponse()
            : base()
        {
        }

        public bool CaptchaNeeded { get; set; }
    }
}