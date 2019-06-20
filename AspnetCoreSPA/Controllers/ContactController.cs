using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Common;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Controllers
{
    //[Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepo
            //= new TestContactRepository();
            = new CsvContactRepository();

        [Route("contacts")]
        // The ResponseCache attribute is used here to prevent browsers from caching the response. 
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult> Contacts()
        {
            List<Contact> response = await _contactRepo.GetAllContacts();
            return Json(response);
        }

        [Route("contacts/search/{filter}")]
        // The ResponseCache attribute is used here to prevent browsers from caching the response. 
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult> Search(string filter = "")
        {
            List<Contact> response = await _contactRepo.GetContacts(filter);
            return Json(response);
        }
    }
}
