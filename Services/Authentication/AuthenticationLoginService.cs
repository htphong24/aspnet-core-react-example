using System;
using System.Threading.Tasks;
using BotDetect.Web;
// ReSharper disable CheckNamespace

namespace Services
{
    public class AuthenticationLoginService : ServiceBase<AuthenticationLoginRequest, AuthenticationLoginResponse>
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationLoginService(ServiceContext context, IAuthenticationRepository authRepo)
            : base(context)
        {
            _authRepo = authRepo;
        }

        /// <summary>
        /// Lists the results of a client search.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<AuthenticationLoginResponse> DoRunAsync(AuthenticationLoginRequest rq)
        {
            var rs = new AuthenticationLoginResponse
            {
                AccessToken = await _authRepo.LoginAsync(rq)
            };

            return rs;
        }
    }
}