using Microsoft.AspNetCore.Mvc;
// ReSharper disable CheckNamespace

namespace Services
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