using AutoMapper;
using Common.Identity;
using Common.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace

namespace Services
{
    public class SqlServerUserRepository : RepositoryBase, IUserRepository, IUserModificationRepository
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<ApplicationRole> _roleMgr;

        public SqlServerUserRepository(
            ContactsMgmtContext db,
            ContactsMgmtIdentityContext idDb,
            IMapper mapper,
            UserManager<ApplicationUser> userMgr,
            RoleManager<ApplicationRole> roleMgr
        ) : base(db, idDb, mapper)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task<List<UserModel>> ListAsync(UserListRequest rq)
        {
            // Create query
            var query = _userMgr.Users;

            // Retrieve data
            var users = await query.ToListAsync();

            // Map to model
            var dtoList = Mapper.Map<List<UserModel>>(users);

            return dtoList;
        }

        public async Task<int> ListRecordCountAsync()
        {
            // Create query
            var query = _userMgr.Users;

            // Retrieve data
            int recordCount = await query.CountAsync();

            return recordCount;
        }

        public async Task<UserModel> GetAsync(UserGetRequest rq)
        {
            // Retrieve data
            var user = await _userMgr.FindByIdAsync(rq.Id);

            // Map to model
            var dto = Mapper.Map<UserModel>(user);

            return dto;
        }

        public async Task CreateAsync(UserCreateRequest rq)
        {
            var dto = rq.User;

            // Handle user
            var user = Mapper.Map<ApplicationUser>(dto);

            // Handle creation
            var result = await _userMgr.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new InvalidOperationException(Utilities.GetIdentityErrors(result.Errors));

            // Handle roles
            result = await _userMgr.AddToRoleAsync(user, ApplicationConstants.ROLE_STANDARD);

            if (!result.Succeeded)
            {
                await _userMgr.DeleteAsync(user);

                throw new InvalidOperationException(Utilities.GetIdentityErrors(result.Errors));
            }
        }

        public async Task UpdateAsync(UserUpdateRequest rq)
        {
            var dto = rq.User;
            var user = await _userMgr.FindByIdAsync(dto.Id);

            // Map to model
            //ApplicationUser user2 = _mapper.Map<ApplicationUser>(dto);
            // Don't allow user to change email as it's used as username
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UpdatedTime = DateTime.UtcNow;

            // Update user
            var updateResult = await _userMgr.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new InvalidOperationException(Utilities.GetIdentityErrors(updateResult.Errors));

            //IList<string> roles = await _userMgr.GetRolesAsync(user);
            //IdentityResult removeResult = await _userMgr.RemoveFromRolesAsync(user, roles);
            //if (!removeResult.Success)
            //{
            //    throw new InvalidOperationException(Utilities.GetIdentityErrors(removeResult.Errors));
            //}

            //IdentityResult addResult = await _userMgr.AddToRoleAsync(user, rq.MainRole);
            //if (!addResult.Success)
            //{
            //    throw new InvalidOperationException(Utilities.GetIdentityErrors(removeResult.Errors));
            //}
        }

        public async Task DeleteAsync(UserDeleteRequest rq)
        {
            var dto = rq.User;
            var user = await _userMgr.FindByIdAsync(dto.Id);
            var deleteResult = await _userMgr.DeleteAsync(user);

            if (!deleteResult.Succeeded)
                throw new InvalidOperationException(Utilities.GetIdentityErrors(deleteResult.Errors));
        }

    }
}