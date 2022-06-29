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

        // http://localhost:xxxx/api/v1/auth
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get([FromRoute] AuthenticationLoginGetRequest rq)
        {
            try
            {
                var rs = new AuthenticationLoginGetResponse
                {
                    CaptchaNeeded = HttpContext.Session.GetInt32("FailedLoginCount") > _authConfig.MaxAttempt
                };

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:xxxx/api/v1/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthenticationLoginRequest rq)
        {
            var failedLoginCount = GetInt32Session("FailedLoginCount");
            rq.IpAddress = GetIpAddress();

            try
            {
                var rs = await new AuthenticationLoginService(Context, _authRepo).RunAsync(rq);
                failedLoginCount = rs.AuthLogin.Success ? 0 : failedLoginCount + 1;
                rs.CaptchaNeeded = failedLoginCount > _authConfig.MaxAttempt;
                SetInt32Session("FailedLoginCount", failedLoginCount);

                if (rs.AuthLogin.Success)
                {
                    AddCookie("accessToken", rs.AuthLogin.AccessToken);
                    AddCookie("refreshToken", rs.AuthLogin.RefreshToken);
                }

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                // Should we consider exception as a failed login? If yes, uncomment out this line
                //SetFailedLoginCount(failedLoginCount++);

                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:xxxx/api/v1/auth/refresh-token
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken()
        {
            try
            {
                var rq = new AuthenticationTokenRefreshRequest
                {
                    Token = Request.Cookies["refreshToken"],
                    IpAddress = GetIpAddress()
                };
                var rs = await new AuthenticationTokenRefreshService(Context, _authRepo).RunAsync(rq);

                if (rs.AuthTokenRefresh.Success)
                {
                    AddCookie("accessToken", rs.AuthTokenRefresh.AccessToken);
                    AddCookie("refreshToken", rs.AuthTokenRefresh.RefreshToken);
                }

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }

        // http://localhost:xxxx/api/v1/auth/logout
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody] AuthenticationLogoutRequest rq)
        {
            try
            {
                rq.RefreshToken = rq.RefreshToken ?? Request.Cookies["refreshToken"]; // accept refreshToken from request body or cookie
                rq.IpAddress = GetIpAddress();

                var rs = await new AuthenticationLogoutService(Context, _authRepo).RunAsync(rq);

                DeleteCookie("accessToken");

                return new ApiActionResult(Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(Context.Request, ex);
            }
        }

        #region Private Methods

        private int GetInt32Session(string name)
        {
            return HttpContext.Session.GetInt32(name) is null
            ? 0
            : (int)HttpContext.Session.GetInt32(name);
        }

        private void SetInt32Session(string name, int value)
        {
            HttpContext.Session.SetInt32(name, value);
        }

        private void AddCookie(string name, string value)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // cookie must not be accessible by client-side script
                Expires = DateTime.UtcNow.AddDays(7),
            };
            Response.Cookies.Append(name, value, cookieOptions);
        }

        private void DeleteCookie(string name)
        {
            Response.Cookies.Delete(name);
        }

        private string GetIpAddress()
        {
            return Request.Headers.ContainsKey("X-Forwarded-For")
                ? Request.Headers["X-Forwarded-For"]
                : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        #endregion Private Methods
    }
}
