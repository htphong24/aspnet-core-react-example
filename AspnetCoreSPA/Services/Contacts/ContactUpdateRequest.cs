using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Contacts
{
    public class ContactUpdateRequest : RequestBase
    {
        public ContactModel Contact { get; set; }
    }
}