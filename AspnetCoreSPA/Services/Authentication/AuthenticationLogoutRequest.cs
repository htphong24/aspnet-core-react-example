using AspnetCoreSPATemplate.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services.Authentication
{
    [BindProperties(SupportsGet = true)]
    public class AuthenticationLogoutRequest : RequestBase
    {
        public AuthenticationLogoutRequest()
            : base()
        {


        }

        public string Email { get; set; }

    }
}
