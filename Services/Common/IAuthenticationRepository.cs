using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
{
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// Logs in, if successfully then returns a token
        /// </summary>
        /// <returns></returns>
        Task<AuthenticationLoginModel> LoginAsync(AuthenticationLoginRequest rq);

        /// <summary>
        /// Logs out
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task LogoutAsync(AuthenticationLogoutRequest rq);
    }
}