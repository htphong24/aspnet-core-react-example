using System.Collections.Generic;
// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactListResponse : ListResponse
    {
        public ContactListResponse()
            : base()
        {
        }

        /// <summary>
        /// Results of the search
        /// </summary>
        public List<ContactModel> Results { get; set; }
    }
}