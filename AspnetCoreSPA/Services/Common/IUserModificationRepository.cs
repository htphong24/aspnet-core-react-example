using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IUserModificationRepository
    {
        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task UpdateAsync(UserUpdateRequest rq);

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task DeleteAsync(UserDeleteRequest rq);
    }
}
