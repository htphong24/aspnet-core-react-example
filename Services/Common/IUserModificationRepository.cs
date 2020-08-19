using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
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