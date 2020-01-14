using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Users;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task<UserModel> GetAsync(UserGetRequest rq);

        /// <summary>
        /// Returns list of users
        /// </summary>
        /// <returns></returns>
        Task<List<UserModel>> ListAsync(UserListRequest rq);

        /// <summary>
        /// Returns total records count of list of users
        /// </summary>
        /// <returns></returns>
        Task<int> ListRecordCountAsync();

        /// <summary>
        /// Creates new user, if successfully then returns a token
        /// </summary>
        /// <returns></returns>
        Task CreateAsync(UserCreateRequest rq);

    }
}
