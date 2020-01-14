using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Contacts;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IContactRepository
    {
        /// <summary>
        /// Returns list of contacts
        /// </summary>
        /// <returns></returns>
        Task<List<ContactModel>> ListAsync(ContactListRequest rq);

        /// <summary>
        /// Returns total records count of list of contacts
        /// </summary>
        /// <returns></returns>
        Task<int> ListRecordCountAsync();

        /// <summary>
        /// Returns search result of contacts
        /// </summary>
        /// <returns></returns>
        Task<List<ContactModel>> SearchAsync(ContactSearchRequest rq);

        /// <summary>
        /// Returns record count of search result of contacts
        /// </summary>
        /// <returns></returns>
        Task<int> SearchRecordCountAsync(ContactSearchRequest rq);

        /// <summary>
        /// Creates a new contact
        /// </summary>
        /// <returns></returns>
        Task CreateAsync(ContactCreateRequest rq);

    }
}
