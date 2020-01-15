using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using System.Collections.Generic;

namespace AspnetCoreSPATemplate.Services.Contacts
{
    public class ContactSearchResponse : SearchResponse
    {
        public ContactSearchResponse()
            : base()
        {
        }

        /// <summary>
        /// Results of the search
        /// </summary>
        public List<ContactModel> Results { get; set; }
    }
}