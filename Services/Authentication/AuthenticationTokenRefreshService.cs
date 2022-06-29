using System;
using System.Threading.Tasks;
using BotDetect.Web;
// ReSharper disable CheckNamespace

namespace Services
{
    public class AuthenticationTokenRefreshService : ServiceBase<AuthenticationTokenRefreshRequest, AuthenticationTokenRefreshResponse>
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationTokenRefreshService(ServiceContext context, IAuthenticationRepository authRepo)
            : base(context)
        {
            _authRepo = authRepo;
        }

        /// <summary>
        /// Lists the results of a client search.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<AuthenticationTokenRefreshResponse> DoRunAsync(AuthenticationTokenRefreshRequest rq)
        {
            var rs = new AuthenticationTokenRefreshResponse
            {
                AuthTokenRefresh = await _authRepo.RefreshTokenAsync(rq),
            };

            return rs;
        }
    }
}