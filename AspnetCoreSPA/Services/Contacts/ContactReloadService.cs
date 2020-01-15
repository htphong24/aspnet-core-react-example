using AspnetCoreSPATemplate.Services.Common;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Contacts
{
    public class ContactReloadService : ServiceBase<ContactReloadRequest, ContactReloadResponse>
    {
        private readonly IContactModificationRepository _contactModRepo;

        public ContactReloadService(ServiceContext context, IContactModificationRepository contactModRepo)
            : base(context)
        {
            _contactModRepo = contactModRepo;
        }

        /// <summary>
        /// Inserts a new contact.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<ContactReloadResponse> DoRunAsync(ContactReloadRequest rq)
        {
            ContactReloadResponse rs = new ContactReloadResponse();
            await _contactModRepo.ReloadAsync(rq);
            return rs;
        }
    }
}