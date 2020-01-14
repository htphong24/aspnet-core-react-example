using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Contacts
{
    public class ContactDeleteRequest : RequestBase
    {
        public ContactModel Contact { get; set; }
    }
}
