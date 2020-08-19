using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
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
            var rs = new ContactListResponse
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