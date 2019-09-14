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
    public class UserGetService : ServiceBase<UserGetRequest, UserGetResponse>
    {
        private readonly IUserModificationRepository _userModRepo;
        private readonly ServiceContext _context;

        public UserGetService(ServiceContext context, IUserModificationRepository userRepo)
            : base(context)
        {
            _userModRepo = userRepo;
            _context = context;
        }

        /// <summary> 
        /// Lists the results of a client search.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<UserGetResponse> DoRunAsync(UserGetRequest rq)
        {
            UserGetResponse rs = new UserGetResponse();
            rs.User = await _userModRepo.GetAsync(rq);
            return rs;
        }
    }
}
