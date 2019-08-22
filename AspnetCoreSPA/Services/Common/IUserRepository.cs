using AspnetCoreSPATemplate.Models;
using Common.Identity;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IUserRepository
    {
        /// <summary>
        /// Creates new user, if successfully then returns a token
        /// </summary>
        /// <returns></returns>
        Task<ApplicationUser> CreateAsync(UserCreateRequest rq);

    }
}
