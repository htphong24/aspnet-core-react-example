using AspnetCoreSPATemplate.Models;
using AutoMapper;
using SqlServerDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Contacts;

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
