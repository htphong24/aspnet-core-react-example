using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Models;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Services;

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
        public Task<ActionResult> List([FromQuery]ContactListRequest request)
        {
            Task<ContactListResponse> response = (new ContactListService(this.Context, _contactRepo)).RunAsync(request);
            return Task.FromResult<ActionResult>(new ApiActionResult(this.Context.Request, response));
        }

        [HttpGet("search")]
        public Task<ActionResult> Search([FromQuery]ContactSearchRequest request)
        {
            Task<ContactSearchResponse> response = (new ContactSearchService(this.Context, _contactRepo)).RunAsync(request);
            return Task.FromResult<ActionResult>(new ApiActionResult(this.Context.Request, response));
        }
    }
}
