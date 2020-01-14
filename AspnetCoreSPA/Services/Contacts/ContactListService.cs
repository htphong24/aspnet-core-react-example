using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Contacts
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
        protected override async Task<ContactListResponse> DoRunAsync(ContactListRequest rq)
        {
            ContactListResponse rs = new ContactListResponse
            {
                Results = await _contactRepo.ListAsync(rq),
                RecordCount = await _contactRepo.ListRecordCountAsync(),
                PageNumber = rq.PageNumber
            };
            rs.PageCount = (rs.RecordCount + rq.RowsPerPage - 1) / rq.RowsPerPage;
            return rs;
        }
    }
}
