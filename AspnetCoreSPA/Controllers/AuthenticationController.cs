using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Utilities;
using Common.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationConfiguration _authConfig;
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationController(IOptions<AuthenticationConfiguration> authConfig, IAuthenticationRepository authRepo)
        {
            _authConfig = authConfig.Value;
            _authRepo = authRepo;
        }

        // http://localhost:7101/api/v1/auth
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get([FromRoute] AuthenticationLoginGetRequest rq)
        {
            try
            {
                //AuthenticationLoginGetResponse rs = await new AuthenticationLoginGetService(Context, _authRepo).RunAsync(rq);
                var rs = new AuthenticationLoginGetResponse
                {
                    CaptchaNeeded = HttpContext.Session.GetInt32("LoginFailedCount") > _authConfig.MaxAttempt
                };

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:7101/api/v1/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthenticationLoginRequest rq)
        {
            var loginFailedCount = HttpContext.Session.GetInt32("LoginFailedCount");
            loginFailedCount ??= 0;

            try
            {
                var rs = await new AuthenticationLoginService(Context, _authRepo).RunAsync(rq);
                loginFailedCount = rs.AuthLogin.Successful ? 0 : loginFailedCount.Value + 1;
                rs.CaptchaNeeded = loginFailedCount > _authConfig.MaxAttempt;
                HttpContext.Session.SetInt32("LoginFailedCount", loginFailedCount.Value);

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetInt32("LoginFailedCount", loginFailedCount.Value + 1);

                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:7101/api/v1/auth/logout
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody] AuthenticationLogoutRequest rq)
        {
            try
            {
                var rs = await new AuthenticationLogoutService(Context, _authRepo).RunAsync(rq);

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }
    }
}
