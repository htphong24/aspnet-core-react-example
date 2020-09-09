using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Utilities;
using BotDetect.Web;
using Microsoft.AspNetCore.Http;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationController(IAuthenticationRepository authRepo)
        {
            _authRepo = authRepo;
        }

        // http://localhost:5000/api/v1/auth
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get([FromRoute] AuthenticationLoginGetRequest rq)
        {
            try
            {
                //AuthenticationLoginGetResponse rs = await new AuthenticationLoginGetService(Context, _authRepo).RunAsync(rq);
                var rs = new AuthenticationLoginGetResponse
                {
                    CaptchaNeeded = HttpContext.Session.GetInt32("LoginFailedCount") > 0
                };

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthenticationLoginRequest rq)
        {
            var loginCount = HttpContext.Session.GetInt32("LoginFailedCount");
            loginCount ??= 0;

            try
            {
                AuthenticationLoginResponse rs = await new AuthenticationLoginService(Context, _authRepo).RunAsync(rq);
                HttpContext.Session.SetInt32("LoginFailedCount", 0);

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetInt32("LoginFailedCount", loginCount.Value + 1);

                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/auth/logout
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody] AuthenticationLogoutRequest rq)
        {
            try
            {
                AuthenticationLogoutResponse rs = await new AuthenticationLogoutService(Context, _authRepo).RunAsync(rq);

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }
    }
}
