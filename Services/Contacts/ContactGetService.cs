using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactGetService : ServiceBase<ContactGetRequest, ContactGetResponse>
    {
        private readonly IContactModificationRepository _contactModRepo;

        public ContactGetService(ServiceContext context, IContactModificationRepository contactRepo)
            : base(context)
        {
            _contactModRepo = contactRepo;
        }

        /// <summary>
        /// Lists the results of a client search.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<ContactGetResponse> DoRunAsync(ContactGetRequest rq)
        {
            var rs = new ContactGetResponse
            {
                Contact = await _contactModRepo.GetAsync(rq)
            };

            return rs;
        }
    }
}