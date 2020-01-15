using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Me;
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