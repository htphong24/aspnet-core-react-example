using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Authentication
{
    public class AuthenticationLogoutService : ServiceBase<AuthenticationLogoutRequest, AuthenticationLogoutResponse>
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationLogoutService(ServiceContext context, IAuthenticationRepository authRepo)
            : base(context)
        {
            _authRepo = authRepo;
        }

        /// <summary>
        /// Lists the results of a client search.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<AuthenticationLogoutResponse> DoRunAsync(AuthenticationLogoutRequest rq)
        {
            AuthenticationLogoutResponse rs = new AuthenticationLogoutResponse();
            await _authRepo.LogoutAsync(rq);
            return rs;
        }
    }
}
