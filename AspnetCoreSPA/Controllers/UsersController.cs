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
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(
            IUserRepository userRepo
        )
        {
            _userRepo = userRepo;
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

        //// http://localhost:5000/api/v1/users/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult> Get([FromRoute]UserGetRequest rq)
        //{
        //    try
        //    {
        //        UserGetResponse rs = await (new UserGetService(this.Context, _userRepo)).RunAsync(rq);
        //        return new ApiActionResult(this.Context.Request, rs);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiActionResult(this.Context.Request, ex);
        //    }
        //}

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


    }
}
