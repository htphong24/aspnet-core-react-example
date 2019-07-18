using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public interface IContactRepository
    {
        /// <summary>
        /// Returns list of contacts
        /// </summary>
        /// <returns></returns>
        Task<IList<Contact>> ListAsync(ContactListRequest request);

        /// <summary>
        /// Returns page count of list of contacts
        /// </summary>
        /// <returns></returns>
        Task<int> ListPageCountAsync(ContactListRequest request);

        /// <summary>
        /// Returns search result of contacts
        /// </summary>
        /// <returns></returns>
        Task<IList<Contact>> SearchAsync(ContactSearchRequest request);

        /// <summary>
        /// Returns record count of search result of contacts
        /// </summary>
        /// <returns></returns>
        Task<int> SearchRecordCountAsync(ContactSearchRequest request);
    }
}
