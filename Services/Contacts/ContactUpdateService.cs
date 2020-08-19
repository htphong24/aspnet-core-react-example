using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactUpdateService : ServiceBase<ContactUpdateRequest, ContactUpdateResponse>
    {
        private readonly IContactModificationRepository _contactModRepo;

        public ContactUpdateService(ServiceContext context, IContactModificationRepository contactModRepo)
            : base(context)
        {
            _contactModRepo = contactModRepo;
        }

        /// <summary>
        /// Inserts a new contact.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<ContactUpdateResponse> DoRunAsync(ContactUpdateRequest rq)
        {
            ContactUpdateResponse rs = new ContactUpdateResponse
            {
                Contact = rq.Contact
            };
            await _contactModRepo.UpdateAsync(rq);
            return rs;
        }
    }
}