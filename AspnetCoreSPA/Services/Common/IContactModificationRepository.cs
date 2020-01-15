using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Contacts;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IContactModificationRepository
    {
        /// <summary>
        /// Gets a contact
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task<ContactModel> GetAsync(ContactGetRequest rq);

        /// <summary>
        /// Updates a contact
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task UpdateAsync(ContactUpdateRequest rq);

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task DeleteAsync(ContactDeleteRequest rq);

        /// <summary>
        /// Reloads all contacts
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task ReloadAsync(ContactReloadRequest rq);
    }
}