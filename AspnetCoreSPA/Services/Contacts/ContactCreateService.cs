using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Services.Contacts;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
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
            ContactCreateResponse rs = new ContactCreateResponse
            {
                Contact = rq.Contact
            };
            await _contactRepo.CreateAsync(rq);
            return rs;
        }
    }
}