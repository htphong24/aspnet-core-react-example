using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Utilities;
using AutoMapper;
using BotDetect.Web;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationController(
            IMapper mapper,
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
                var captcha = new SimpleCaptcha();
                bool isHuman = captcha.Validate(rq.UserEnteredCaptchaCode, rq.CaptchaId);

                if (!isHuman)
                {
                    var ex = new InvalidOperationException("Incorrect Captcha characters!");

                    return new ApiActionResult(Context.Request, ex);
                }

                AuthenticationLoginResponse rs = await (new AuthenticationLoginService(Context, _authRepo)).RunAsync(rq);

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/auth/logout
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody]AuthenticationLogoutRequest rq)
        {
            try
            {
                AuthenticationLogoutResponse rs = await (new AuthenticationLogoutService(Context, _authRepo)).RunAsync(rq);
                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }
    }
}