using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Services.Users;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserModificationRepository _userModRepo;

        public UsersController(
            IUserRepository userRepo,
            IUserModificationRepository userModRepo
        )
        {
            _userRepo = userRepo;
            _userModRepo = userModRepo;
        }

        // http://localhost:5000/api/v1/users
        [HttpGet]
        public async Task<ActionResult> List([FromQuery]UserListRequest rq)
        {
            try
            {
                UserListResponse rs = await (new UserListService(this.Context, _userRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute]UserGetRequest rq)
        {
            try
            {
                UserGetResponse rs = await (new UserGetService(this.Context, _userRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/users/create
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody]UserCreateRequest rq)
        {
            try
            {
                UserCreateResponse rs = await (new UserCreateService(this.Context, _userRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/users/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody]UserUpdateRequest rq)
        {
            try
            {
                rq.User.Id = id;
                UserUpdateResponse rs = await (new UserUpdateService(this.Context, _userModRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id, [FromBody]UserDeleteRequest rq)
        {
            try
            {
                rq.User.Id = id;
                UserDeleteResponse rs = await (new UserDeleteService(this.Context, _userModRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }
    }
}