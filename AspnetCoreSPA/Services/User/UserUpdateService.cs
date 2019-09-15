using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
{
    public class UserUpdateService : ServiceBase<UserUpdateRequest, UserUpdateResponse>
    {
        private readonly IUserModificationRepository _userModRepo;

        public UserUpdateService(ServiceContext context, IUserModificationRepository userModRepo)
            : base(context)
        {
            _userModRepo = userModRepo;
        }

        /// <summary> 
        /// update a user.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<UserUpdateResponse> DoRunAsync(UserUpdateRequest rq)
        {
            UserUpdateResponse rs = new UserUpdateResponse();
            rs.User = rq.User;
            await _userModRepo.UpdateAsync(rq);
            return rs;
        }
    }
}
