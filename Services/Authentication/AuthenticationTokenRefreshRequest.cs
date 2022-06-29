using Microsoft.AspNetCore.Mvc;
// ReSharper disable CheckNamespace

namespace Services
{
    [BindProperties(SupportsGet = true)]
    public class AuthenticationTokenRefreshRequest : RequestBase
    {
        public AuthenticationTokenRefreshRequest()
            : base()
        {
        }

        public string Token { get; set; }

        public string IpAddress { get; set; }
    }
}