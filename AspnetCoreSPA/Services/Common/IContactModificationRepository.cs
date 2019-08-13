using AspnetCoreSPATemplate.Models;
using AutoMapper;
using SqlServerDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IContactModificationRepository
    {
        /// <summary>
        /// Gets a contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ContactModel> GetAsync(ContactGetRequest rq);

        /// <summary>
        /// Updates a contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task UpdateAsync(ContactUpdateRequest rq);

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task DeleteAsync(ContactDeleteRequest rq);
    }
}
