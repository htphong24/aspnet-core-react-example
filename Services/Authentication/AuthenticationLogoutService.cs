using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
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
            await _authRepo.LogoutAsync(rq);

            return new AuthenticationLogoutResponse();
        }
    }
}