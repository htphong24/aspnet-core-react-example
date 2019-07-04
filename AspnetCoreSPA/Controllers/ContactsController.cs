using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Models;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreSPATemplate.Utils;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/[controller]")]
    public class ContactsController : BaseController
    {
        private readonly IContactRepository _contactRepo;

        public ContactsController(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        [HttpGet]
        public async Task<ActionResult> Contacts()
        {
            IList<Contact> response = await _contactRepo.GetAllContactsAsync();
            return Json(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]SearchRequest request)
        {
            //SearchRequest request = ApiDeserializer.Deserialize(typeof(SearchRequest), this.Context.Request) as SearchRequest;
            //SearchResponse rs = await _contactRepo(this.ServiceContext).RunAsync(request);
            IList<Contact> response = await _contactRepo.GetContactsAsync(request.Query);
            return Json(response);
        }
    }
}
