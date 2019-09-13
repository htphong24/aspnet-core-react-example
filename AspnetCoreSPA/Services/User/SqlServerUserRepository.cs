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

        public async Task<List<UserModel>> ListAsync(UserListRequest rq)
        {
            // Create query
            IQueryable<ApplicationUser> query = _userMgr.Users;
            // Retrieve data
            List<ApplicationUser> users = await query.ToListAsync();
            // Map to model
            List<UserModel> dtoList = _mapper.Map<List<UserModel>>(users);

            return dtoList;
        }

        public async Task<int> ListRecordCountAsync()
        {
            // Create query
            IQueryable<ApplicationUser> query = _userMgr.Users;
            // Retrieve data
            int recordCount = await query.CountAsync();

            return recordCount;
        }

        public async Task CreateAsync(UserCreateRequest rq)
        {
            UserModel dto = rq.User;

            // Handle user
            ApplicationUser user = _mapper.Map<ApplicationUser>(dto);
            //ApplicationUser user = new ApplicationUser()
            //{
            //    UserName = rq.User.Email,
            //    Email = rq.User.Email,
            //    FirstName = rq.User.FirstName,
            //    LastName = rq.User.LastName
            //};

            // Handle creation
            IdentityResult result = await _userMgr.CreateAsync(user, dto.Password);
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
