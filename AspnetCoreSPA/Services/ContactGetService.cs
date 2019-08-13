using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using AutoMapper;
using SqlServerDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
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
            ContactGetResponse rs = new ContactGetResponse();
            rs.Contact = await _contactModRepo.GetAsync(rq);
            return rs;
        }
    }
}
