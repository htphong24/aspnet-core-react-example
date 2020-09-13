using AutoMapper;
using Common.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BotDetect.Web;
using Common.Configuration;

// ReSharper disable CheckNamespace

namespace Services
{
    public class SqlServerAuthenticationRepository : RepositoryBase, IAuthenticationRepository
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<ApplicationRole> _roleMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;

        public SqlServerAuthenticationRepository(
            ContactsMgmtContext db,
            ContactsMgmtIdentityContext idDb,
            IMapper mapper,
            IOptions<JwtConfiguration> jwtOptions,
            UserManager<ApplicationUser> userMgr,
            RoleManager<ApplicationRole> roleMgr,
            SignInManager<ApplicationUser> signInMgr)
            : base(db, idDb, mapper)
        {
            _jwtConfig = jwtOptions.Value;
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _signInMgr = signInMgr;
        }

        public async Task<AuthenticationLoginModel> LoginAsync(AuthenticationLoginRequest rq)
        {
            if (rq.CaptchaNeeded)
            {
                var captcha = new SimpleCaptcha();
                bool isHuman = captcha.Validate(rq.UserEnteredCaptchaCode, rq.CaptchaId);

                if (!isHuman)
                    return new AuthenticationLoginModel { Successful = false, Message = "Incorrect Captcha characters!" };
            }
            //ValidateCaptcha(rq.UserEnteredCaptchaCode, rq.CaptchaId);

            // Handle user
            ApplicationUser user = await _userMgr.FindByNameAsync(rq.Email);

            if (user == null)
                return new AuthenticationLoginModel { Successful = false, Message = "User not found" };

            if (await _userMgr.IsLockedOutAsync(user))
                return new AuthenticationLoginModel { Successful = false, Message = "User is locked out" };

            // Handle signin
            SignInResult result = await _signInMgr.PasswordSignInAsync(rq.Email, rq.Password, true, true);

            if (!result.Succeeded)
                return new AuthenticationLoginModel { Successful = false, Message = "Password is incorrect" };

            // Handle roles
            IList<string> roles = await _userMgr.GetRolesAsync(user);

            return new AuthenticationLoginModel { Successful = true, Message = "Logged in successfully", AccessToken = GenerateJwt(user, roles) };
        }

        public async Task LogoutAsync(AuthenticationLogoutRequest rq)
        {
            // Handle user
            ApplicationUser user = await _userMgr.FindByNameAsync(rq.Email);

            if (user == null)
                throw new InvalidOperationException("User not found");

            // Handle signout
            await _signInMgr.SignOutAsync();
        }

        private string GenerateJwt(ApplicationUser user, IList<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.Role, ServerRole.NormalUser.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.NormalizedUserName)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtConfig.DaysToExpiration),
                signingCredentials: credentials);

            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return serializedToken;
        }

        private void ValidateCaptcha(string userEnteredCaptchaCode, string captchaId)
        {
            var captcha = new SimpleCaptcha();
            bool isHuman = captcha.Validate(userEnteredCaptchaCode, captchaId);

            if (!isHuman)
                throw new InvalidOperationException("Incorrect Captcha characters!");
        }
    }
}
