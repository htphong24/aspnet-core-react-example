using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SqlServerDataAccess;
using AutoMapper;
using SqlServerDataAccess.EF;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Common.Identity;

namespace AspnetCoreSPATemplate.Services
{
    
    public class SqlServerUserRepository : RepositoryBase, IUserRepository
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<ApplicationRole> _roleMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;

        public SqlServerUserRepository(
            ContactsMgmtContext db,
            ContactsMgmtIdentityContext idDb,
            IMapper mapper,
            IOptions<JwtConfiguration> options,
            UserManager<ApplicationUser> userMgr,
            RoleManager<ApplicationRole> roleMgr,
            SignInManager<ApplicationUser> signInMgr)
            : base(db, idDb, mapper)
        {
            _jwtConfig = options.Value;
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _signInMgr = signInMgr;
        }

        public async Task<ApplicationUser> CreateAsync(UserCreateRequest rq)
        {
            // Handle user
            ApplicationUser user = new ApplicationUser()
            {
                UserName = rq.Email,
                Email = rq.Email,
                FirstName = rq.FirstName,
                LastName = rq.LastName
            };

            // Handle creation
            IdentityResult result = await _userMgr.CreateAsync(user, rq.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join('\n', result.Errors.Select(e => $"Error code: {e.Code}. Message: {e.Description}")));
            }

            // Handle roles
            result = await _userMgr.AddToRoleAsync(user, ApplicationConstants.ROLE_STANDARD);
            if (!result.Succeeded)
            {
                await _userMgr.DeleteAsync(user);
                throw new InvalidOperationException(string.Join('\n', result.Errors.Select(e => $"Error code: {e.Code}. Message: {e.Description}")));
            }

            return user;
        }

        private string GenerateJwt(ApplicationUser user, IList<string> roles)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.Role, ServerRole.NormalUser.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.NormalizedUserName)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtConfig.DaysToExpiration),
                signingCredentials: credentials);

            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return serializedToken;
        }
    }
}
