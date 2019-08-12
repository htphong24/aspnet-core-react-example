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
        protected override async Task<ContactSearchResponse> DoRunAsync(ContactSearchRequest rq)
        {
            ContactSearchResponse rs = new ContactSearchResponse();
            rs.Results = await _contactRepo.SearchAsync(rq);
            rs.RecordCount = await _contactRepo.SearchRecordCountAsync(rq);
            rs.PageCount = (rs.RecordCount + rq.RowsPerPage - 1) / rq.RowsPerPage;
            rs.PageNumber = rq.PageNumber;
            return rs;
        }
    }
}
