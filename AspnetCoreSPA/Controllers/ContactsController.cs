using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Services;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Services.Contacts;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireStandard")]
    public class ContactsController : ControllerBase
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

        // http://localhost:5000/api/v1/contacts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute]ContactGetRequest rq)
        {
            try
            {
                ContactGetResponse rs = await (new ContactGetService(this.Context, _contactModRepo)).RunAsync(rq);
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

        // http://localhost:5000/api/v1/contacts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody]ContactUpdateRequest rq)
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

        // http://localhost:5000/api/v1/contacts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id, [FromBody]ContactDeleteRequest rq)
        {
            try
            {
                ContactDeleteResponse rs = await (new ContactDeleteService(this.Context, _contactModRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/contacts/reload
        [HttpPost("reload")]
        public async Task<ActionResult> Reload([FromBody]ContactReloadRequest rq)
        {
            try
            {
                ContactReloadResponse rs = await (new ContactReloadService(this.Context, _contactModRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

    }
}
