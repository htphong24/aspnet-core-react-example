using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
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
            ContactUpdateResponse rs = new ContactUpdateResponse();
            rs.Contact = rq.Contact;
            await _contactModRepo.UpdateAsync(rq);
            return rs;
        }
    }
}
