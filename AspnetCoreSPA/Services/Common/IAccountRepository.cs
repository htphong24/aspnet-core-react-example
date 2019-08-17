using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Logs in, if successfully then returns token
        /// </summary>
        /// <returns></returns>
        Task<string> LoginAsync(AccountLoginRequest rq);

    }
}
