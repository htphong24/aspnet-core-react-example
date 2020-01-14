using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Authentication
{
    public class AuthenticationLoginResponse : ResponseBase
    {
        public AuthenticationLoginResponse()
            : base()
        {

        }

        public string AccessToken { get; set; }

        //public ApplicationUser User { get; set; }

    }
}
