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
using AspnetCoreSPATemplate.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using SqlServerDataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationController(
            IAuthenticationRepository authRepo
        )
        {
            _authRepo = authRepo;
        }

        // http://localhost:5000/api/v1/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]AuthenticationLoginRequest rq)
        {
            try
            {
                AuthenticationLoginResponse rs = await (new AuthenticationLoginService(this.Context, _authRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/auth/logout
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody]AuthenticationLogoutRequest rq)
        {
            try
            {
                AuthenticationLogoutResponse rs = await (new AuthenticationLogoutService(this.Context, _authRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

    }
}
