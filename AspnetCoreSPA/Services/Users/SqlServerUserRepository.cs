using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Services.Common.Configuration;
using AutoMapper;
using Common.Identity;
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

namespace AspnetCoreSPATemplate.Services.Users
{
    public class SqlServerUserRepository : RepositoryBase, IUserRepository, IUserModificationRepository
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

        public async Task<UserModel> GetAsync(UserGetRequest rq)
        {
            // Retrieve data
            ApplicationUser user = await _userMgr.FindByIdAsync(rq.Id);
            // Map to model
            UserModel dto = _mapper.Map<UserModel>(user);

            return dto;
        }

        public async Task CreateAsync(UserCreateRequest rq)
        {
            UserCreateModel dto = rq.User;

            // Handle user
            ApplicationUser user = _mapper.Map<ApplicationUser>(dto);

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

        public async Task UpdateAsync(UserUpdateRequest rq)
        {
            UserUpdateModel dto = rq.User;
            ApplicationUser user = await _userMgr.FindByIdAsync(dto.Id);

            // Map to model
            //ApplicationUser user2 = _mapper.Map<ApplicationUser>(dto);
            // Don't allow to change email as it's used as username
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UpdatedTime = DateTime.UtcNow;

            // Update user
            IdentityResult updateResult = await _userMgr.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join('\n', updateResult.Errors.Select(e => $"Error code: {e.Code}. Message: {e.Description}")));
            }

            //IList<string> roles = await _userMgr.GetRolesAsync(user);
            //IdentityResult removeResult = await _userMgr.RemoveFromRolesAsync(user, roles);
            //if (!removeResult.Succeeded)
            //{
            //    throw new InvalidOperationException(string.Join('\n', updateResult.Errors.Select(e => $"Error code: {e.Code}. Message: {e.Description}")));
            //}

            //IdentityResult addResult = await _userMgr.AddToRoleAsync(user, rq.MainRole);
            //if (!addResult.Succeeded)
            //{
            //    throw new InvalidOperationException(string.Join('\n', updateResult.Errors.Select(e => $"Error code: {e.Code}. Message: {e.Description}")));
            //}
        }

        public async Task DeleteAsync(UserDeleteRequest rq)
        {
            UserDeleteModel dto = rq.User;
            ApplicationUser user = await _userMgr.FindByIdAsync(dto.Id);
            IdentityResult deleteResult = await _userMgr.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join('\n', deleteResult.Errors.Select(e => $"Error code: {e.Code}. Message: {e.Description}")));
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