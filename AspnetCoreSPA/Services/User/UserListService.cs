using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using AutoMapper;
using SqlServerDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
{
    public class UserListService : ServiceBase<UserListRequest, UserListResponse>
    {
        private readonly IUserRepository _userRepo;

        public UserListService(ServiceContext context, IUserRepository userRepo)
            : base(context)
        {
            _userRepo = userRepo;
        }

        /// <summary> 
        /// Lists the results of a client search.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<UserListResponse> DoRunAsync(UserListRequest rq)
        {
            UserListResponse rs = new UserListResponse();
            rs.Results = await _userRepo.ListAsync(rq);
            rs.RecordCount = await _userRepo.ListRecordCountAsync();
            return rs;
        }
    }
}
