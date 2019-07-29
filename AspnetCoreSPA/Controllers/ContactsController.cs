using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Models;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Services;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : BaseController
    {
        private readonly IContactRepository _contactRepo;

        public ContactsController(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        // http://localhost:5000/api/v1/contacts
        [HttpGet]
        public async Task<ActionResult> List([FromQuery]ContactListRequest request)
        {
            try
            {
                ContactListResponse response = await (new ContactListService(this.Context, _contactRepo)).RunAsync(request);
                return new ApiActionResult(this.Context.Request, response);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
            
        }

        // http://localhost:5000/api/v1/contacts/search?q=abc
        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]ContactSearchRequest request)
        {
            try
            {
                ContactSearchResponse response = await (new ContactSearchService(this.Context, _contactRepo)).RunAsync(request);
                return new ApiActionResult(this.Context.Request, response);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
            
        }

        // http://localhost:5000/api/v1/contacts
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ContactCreateRequest request)
        {
            try
            {
                ContactCreateResponse response = await (new ContactCreateService(this.Context, _contactRepo)).RunAsync(request);
                return new ApiActionResult(this.Context.Request, response);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }
    }
}
