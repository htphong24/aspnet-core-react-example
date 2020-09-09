using Microsoft.AspNetCore.Mvc;
// ReSharper disable CheckNamespace

namespace Services
{
    [BindProperties(SupportsGet = true)]
    public class AuthenticationLoginGetRequest : RequestBase
    {
        public AuthenticationLoginGetRequest()
            : base()
        {
        }

    }
}