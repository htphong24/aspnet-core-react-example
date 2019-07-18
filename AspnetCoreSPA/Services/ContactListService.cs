using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
{
    public class ContactListService : ServiceBase<ContactListRequest, ContactListResponse>
    {
        private readonly IContactRepository _contactRepo;

        public ContactListService(ServiceContext context, IContactRepository contactRepo)
            : base(context)
        {
            _contactRepo = contactRepo;
        }

        /// <summary> 
        /// Lists the results of a client search.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<ContactListResponse> DoRunAsync(ContactListRequest request)
        {
            ContactListResponse response = new ContactListResponse();
            response.Results = await _contactRepo.ListAsync(request);
            response.PageCount = await _contactRepo.ListPageCountAsync(request);
            response.PageNumber = request.PageNumber;
            return response;
        }
    }
}
