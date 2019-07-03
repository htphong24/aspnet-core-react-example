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
    //[Route("api/[controller]")]
    public class ContactController : BaseController
    {
        private readonly IContactRepository _contactRepo;

        public ContactController(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        [Route("contacts")]
        public async Task<ActionResult> Contacts()
        {
            IList<Contact> response = await _contactRepo.GetAllContactsAsync();
            return Json(response);
        }

        [Route("contacts/search/{filter}")]
        public async Task<ActionResult> Search(string filter = "")
        {
            SearchRequest request = ApiDeserializer.Deserialize(typeof(SearchRequest), this.Context.Request) as SearchRequest;
            IList<Contact> response = await _contactRepo.GetContactsAsync(filter);
            return Json(response);
        }
    }
}
