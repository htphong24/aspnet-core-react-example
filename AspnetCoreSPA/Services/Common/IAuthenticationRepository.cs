using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// Logs in, if successfully then returns a token
        /// </summary>
        /// <returns></returns>
        Task<string> LoginAsync(AuthenticationLoginRequest rq);

        /// <summary>
        /// Logs out
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task LogoutAsync(AuthenticationLogoutRequest rq);

    }
}
