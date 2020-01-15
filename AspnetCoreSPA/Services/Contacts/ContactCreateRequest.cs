using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Contacts
{
    public class ContactCreateRequest : RequestBase
    {
        public ContactModel Contact { get; set; }
    }
}