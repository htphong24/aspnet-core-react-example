// ReSharper disable CheckNamespace

using System.Text.Json.Serialization;

namespace Services
{
    public class AuthenticationTokenRefreshResponse : ResponseBase
    {
        public AuthenticationTokenRefreshResponse()
            : base()
        {
        }

        public AuthenticationTokenRefreshModel AuthTokenRefresh { get; set; }

        //public ApplicationUser User { get; set; }

        /// <summary>
        /// Refresh token is returned in http only cookie
        /// </summary>
        //public string RefreshToken { get; set; }
    }
}