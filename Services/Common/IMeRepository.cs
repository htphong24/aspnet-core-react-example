using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
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