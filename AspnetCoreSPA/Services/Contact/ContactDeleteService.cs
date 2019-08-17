using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
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
        /// Inserts a new contact.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<ContactDeleteResponse> DoRunAsync(ContactDeleteRequest rq)
        {
            ContactDeleteResponse rs = new ContactDeleteResponse();
            await _contactModRepo.DeleteAsync(rq);
            return rs;
        }
    }
}
