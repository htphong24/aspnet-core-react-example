using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Contacts
{
    public class ContactUpdateResponse: ResponseBase
    {
        public ContactModel Contact { get; set; }
    }
}
