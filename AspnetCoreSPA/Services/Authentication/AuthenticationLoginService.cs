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
    public class AuthenticationLoginService : ServiceBase<AuthenticationLoginRequest, AuthenticationLoginResponse>
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationLoginService(ServiceContext context, IAuthenticationRepository authRepo)
            : base(context)
        {
            _authRepo = authRepo;
        }

        /// <summary> 
        /// Lists the results of a client search.
        /// </summary> 
        /// <param name="rq">Request</param> 
        /// <returns>Response</returns>
        protected override async Task<AuthenticationLoginResponse> DoRunAsync(AuthenticationLoginRequest rq)
        {
            AuthenticationLoginResponse rs = new AuthenticationLoginResponse();
            rs.AccessToken = await _authRepo.LoginAsync(rq);
            return rs;
        }
    }
}
