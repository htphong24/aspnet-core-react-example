using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Common
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContacts();
        Task<List<Contact>> GetContacts(string filter);
    }
}
