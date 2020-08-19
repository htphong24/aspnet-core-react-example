using System.Collections.Generic;
// ReSharper disable CheckNamespace

namespace Services
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