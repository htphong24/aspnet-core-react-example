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
    public class AccountLoginService : ServiceBase<AccountLoginRequest, AccountLoginResponse>
    {
        private readonly IAccountRepository _accRepo;

        public AccountLoginService(ServiceContext context, IAccountRepository accRepo)
            : base(context)
        {
            _accRepo = accRepo;
        }

        /// <summary> 
        /// Lists the results of a client search.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<AccountLoginResponse> DoRunAsync(AccountLoginRequest rq)
        {
            AccountLoginResponse rs = new AccountLoginResponse();
            rs.Token = await _accRepo.LoginAsync(rq);
            return rs;
        }
    }
}
