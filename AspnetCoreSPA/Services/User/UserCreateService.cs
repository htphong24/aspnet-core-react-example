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
    public class UserCreateService : ServiceBase<UserCreateRequest, UserCreateResponse>
    {
        private readonly IUserRepository _userRepo;

        public UserCreateService(ServiceContext context, IUserRepository userRepo)
            : base(context)
        {
            _userRepo = userRepo;
        }

        /// <summary> 
        /// Lists the results of a client search.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<UserCreateResponse> DoRunAsync(UserCreateRequest rq)
        {
            UserCreateResponse rs = new UserCreateResponse();
            rs.User = await _userRepo.CreateAsync(rq);
            return rs;
        }
    }
}
