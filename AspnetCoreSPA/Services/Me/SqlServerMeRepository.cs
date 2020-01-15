using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Services.Common.Configuration;
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

namespace AspnetCoreSPATemplate.Services.Me
{
    public class SqlServerMeRepository : RepositoryBase, IMeRepository
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<ApplicationRole> _roleMgr;
        private readonly SignInManager<ApplicationUser> _signInMgr;

        public SqlServerMeRepository(
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

        public async Task<UserModel> GetAsync(MeGetRequest rq)
        {
            // Retrieve data
            ApplicationUser user = await _userMgr.FindByIdAsync(rq.Id);
            // Map to model
            UserModel dto = _mapper.Map<UserModel>(user);

            return dto;
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