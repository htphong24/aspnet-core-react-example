using AutoMapper;
using Common.Configuration;
using BotDetect.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

// ReSharper disable CheckNamespace

namespace Services
{
    public class SqlServerAuthenticationRepository : RepositoryBase, IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        //private readonly RoleManager<ApplicationRole> _roleMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;
        private readonly IJwtFactory _jwtFactory;
        private readonly IRefreshTokenFactory _refreshTokenFactory;

        public SqlServerAuthenticationRepository(
            ContactsMgmtContext db,
            ContactsMgmtIdentityContext idDb,
            IMapper mapper,
            IOptions<JwtConfiguration> jwtOptions,
            UserManager<ApplicationUser> userMgr,
            RoleManager<ApplicationRole> roleMgr,
            SignInManager<ApplicationUser> signInMgr,
            IJwtFactory jwtFactory,
            IRefreshTokenFactory refreshTokenFactory
        ) : base(db, idDb, mapper)
        {
            _userMgr = userMgr;
            //_roleMgr = roleMgr;
            _signInMgr = signInMgr;
            _jwtFactory = jwtFactory;
            _refreshTokenFactory = refreshTokenFactory;
        }

        public async Task<AuthenticationLoginModel> LoginAsync(AuthenticationLoginRequest rq)
        {
            // Validate Captcha
            if (rq.CaptchaNeeded && !IsCaptchaValid(rq.UserEnteredCaptchaCode, rq.CaptchaId))
                return new AuthenticationLoginModel(false, "Incorrect Captcha characters!");

            // Validate user
            var user = await _userMgr.FindByNameAsync(rq.Email);

            if (user is null)
                return new AuthenticationLoginModel(false, "User not found");

            if (await _userMgr.IsLockedOutAsync(user))
                return new AuthenticationLoginModel(false, "User is locked out");

            // Sign in
            var result = await _signInMgr.CheckPasswordSignInAsync(user, rq.Password, true);

            if (!result.Succeeded)
                return new AuthenticationLoginModel(false, "Unable to log in");

            // Retrieve roles
            var roles = await _userMgr.GetRolesAsync(user);

            // Authentication successful so generate jwt and refresh tokens
            var jwt = _jwtFactory.GenerateJwt(user, roles);
            var refreshToken = _refreshTokenFactory.GenerateRefreshToken(rq.IpAddress);

            // Save refresh token
            user.RefreshTokens.Add(refreshToken);
            var updateResult = await _userMgr.UpdateAsync(user);

            return updateResult.Succeeded
                ? new AuthenticationLoginModel(true, "Logged in successfully") { Email = rq.Email, AccessToken = jwt, RefreshToken = refreshToken.Token }
                : new AuthenticationLoginModel(false, Utilities.GetIdentityErrors(updateResult.Errors));
        }

        public async Task<AuthenticationTokenRefreshModel> RefreshTokenAsync(AuthenticationTokenRefreshRequest rq)
        {
            // Retrieve user with refresh token
            var user = _userMgr.Users
                .Include(user => user.RefreshTokens)
                .SingleOrDefault(user => user.RefreshTokens.Any(refreshToken => refreshToken.Token == rq.Token));

            // Return null if no user found with refresh token
            if (user is null)
                return new AuthenticationTokenRefreshModel(false, "No user found with token");

            // Retrieve refresh token
            var refreshToken = user.RefreshTokens.SingleOrDefault(refreshToken => refreshToken.Token == rq.Token);

            if (refreshToken is null)
                return new AuthenticationTokenRefreshModel(false, "Error retrieving refresh token");

            // Return null if token is no longer active
            if (!refreshToken.IsActive)
                return new AuthenticationTokenRefreshModel(false, "Refresh token is no longer active"); ;

            // Replace old refresh token with a new one
            var newRefreshToken = _refreshTokenFactory.GenerateRefreshToken(rq.IpAddress);
            refreshToken.RevokedTime = DateTime.UtcNow;
            refreshToken.RevokedByIp = rq.IpAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            // Retrieve roles
            var roles = await _userMgr.GetRolesAsync(user);

            // Generate new jwt
            var jwt = _jwtFactory.GenerateJwt(user, roles);

            // Then save...
            user.RefreshTokens.Add(newRefreshToken);
            var updateResult = await _userMgr.UpdateAsync(user);

            return updateResult.Succeeded
                ? new AuthenticationTokenRefreshModel(true, "Refreshed token successfully") { AccessToken = jwt, RefreshToken = newRefreshToken.Token }
                : new AuthenticationTokenRefreshModel(false, Utilities.GetIdentityErrors(updateResult.Errors));
        }

        public async Task LogoutAsync(AuthenticationLogoutRequest rq)
        {
            await RevokeToken(rq.RefreshToken, rq.IpAddress);
        }

        #region Private Methods

        private bool IsCaptchaValid(string userEnteredCaptchaCode, string captchaId)
        {
            return new SimpleCaptcha().Validate(userEnteredCaptchaCode, captchaId);
        }

        private async Task RevokeToken(string refreshToken, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new InvalidOperationException("Token is required");

            // Retrieve user with refresh token
            var user = _userMgr.Users
                .Include(user => user.RefreshTokens)
                .SingleOrDefault(user => user.RefreshTokens.Any(token => token.Token == refreshToken));

            // Throw exception if no user found with token
            if (user is null)
                throw new InvalidOperationException("No user found with token");

            // Throw exception if user has no token
            if (!user.RefreshTokens.Any())
                throw new InvalidOperationException("User has no token");

            // Retrieve refresh token
            var token = user.RefreshTokens.SingleOrDefault(token => token.Token == refreshToken);

            // Throw exception if token is no longer active
            if (!token.IsActive)
                throw new InvalidOperationException("Token is no longer active");

            // Revoke token and save
            token.RevokedTime = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;

            // Then save...
            var updateResult = await _userMgr.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new InvalidOperationException(Utilities.GetIdentityErrors(updateResult.Errors));
        }

        #endregion Private Methods
    }
}
