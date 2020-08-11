using AspnetCoreSPATemplate.Services.Authentication;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotDetect.Web;

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
        public async Task<ActionResult> Login([FromBody] AuthenticationLoginRequest rq)
        {
            try
            {
                SimpleCaptcha captcha = new SimpleCaptcha();
                bool isHuman = captcha.Validate(rq.UserEnteredCaptchaCode, rq.CaptchaId);

                if (!isHuman)
                {
                    var ex = new InvalidOperationException("Incorrect Captcha characters!");

                    return new ApiActionResult(this.Context.Request, ex);
                }

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