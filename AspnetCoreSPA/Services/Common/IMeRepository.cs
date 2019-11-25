using AspnetCoreSPATemplate.Models;
using Common.Identity;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IMeRepository
    {
        /// <summary>
        /// Gets current user
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task<UserModel> GetAsync(MeGetRequest rq);

    }
}
