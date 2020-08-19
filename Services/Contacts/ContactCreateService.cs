using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactCreateService : ServiceBase<ContactCreateRequest, ContactCreateResponse>
    {
        private readonly IContactRepository _contactRepo;

        public ContactCreateService(ServiceContext context, IContactRepository contactRepo)
            : base(context)
        {
            _contactRepo = contactRepo;
        }

        /// <summary>
        /// Inserts a new contact.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<ContactCreateResponse> DoRunAsync(ContactCreateRequest rq)
        {
            var rs = new ContactCreateResponse
            {
                Contact = rq.Contact
            };
            await _contactRepo.CreateAsync(rq);
            return rs;
        }
    }
}