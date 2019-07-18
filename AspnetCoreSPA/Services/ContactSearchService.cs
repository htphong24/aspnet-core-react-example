using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
{
    public class ContactSearchService : ServiceBase<ContactSearchRequest, ContactSearchResponse>
    {
        private readonly IContactRepository _contactRepo;

        public ContactSearchService(ServiceContext context, IContactRepository contactRepo)
            : base(context)
        {
            _contactRepo = contactRepo;
        }

        /// <summary> 
        /// Lists the results of a client search.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<ContactSearchResponse> DoRunAsync(ContactSearchRequest request)
        {
            ContactSearchResponse response = new ContactSearchResponse();
            response.Results = await _contactRepo.SearchAsync(request);
            response.RecordCount = await _contactRepo.SearchRecordCountAsync(request);
            response.PageCount = (response.RecordCount + request.RowsPerPage - 1) / request.RowsPerPage;
            response.PageNumber = request.PageNumber;
            return response;
        }
    }
}
