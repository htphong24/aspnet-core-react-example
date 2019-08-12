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
using AutoMapper;
using SqlServerDataAccess;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : BaseController
    {
        private readonly IContactRepository _contactRepo;
        private readonly IContactModificationRepository _contactModRepo;

        public ContactsController(
            IContactRepository contactRepo, 
            IContactModificationRepository contactModRepo
        )
        {
            _contactRepo = contactRepo;
            _contactModRepo = contactModRepo;
        }

        // http://localhost:5000/api/v1/contacts
        [HttpGet]
        public async Task<ActionResult> List([FromQuery]ContactListRequest rq)
        {
            try
            {
                ContactListResponse rs = await (new ContactListService(this.Context, _contactRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
            
        }

        // http://localhost:5000/api/v1/contacts/search?q=abc
        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]ContactSearchRequest rq)
        {
            try
            {
                ContactSearchResponse rs = await (new ContactSearchService(this.Context, _contactRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
            
        }

        // http://localhost:5000/api/v1/contacts
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ContactCreateRequest rq)
        {
            try
            {
                ContactCreateResponse rs = await (new ContactCreateService(this.Context, _contactRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/contacts
        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ContactUpdateRequest rq)
        {
            try
            {
                ContactUpdateResponse rs = await (new ContactUpdateService(this.Context, _contactModRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        
    }
}
