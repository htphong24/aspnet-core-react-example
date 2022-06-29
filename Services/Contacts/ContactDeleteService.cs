using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactDeleteService : ServiceBase<ContactDeleteRequest, ContactDeleteResponse>
    {
        private readonly IContactModificationRepository _contactModRepo;

        public ContactDeleteService(ServiceContext context, IContactModificationRepository contactModRepo)
            : base(context)
        {
            _contactModRepo = contactModRepo;
        }

        /// <summary>
        /// Delete contact.
        /// </summary>
        /// <param name="rq">Request</param>
        /// <returns>Response</returns>
        protected override async Task<ContactDeleteResponse> DoRunAsync(ContactDeleteRequest rq)
        {
            var rs = new ContactDeleteResponse();
            await _contactModRepo.DeleteAsync(rq);
            return rs;
        }
    }
}