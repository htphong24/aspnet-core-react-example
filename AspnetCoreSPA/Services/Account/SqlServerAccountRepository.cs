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

namespace AspnetCoreSPATemplate.Services
{
    
    public class SqlServerAccountRepository : RepositoryBase, IAccountRepository
    {
        private readonly JwtConfiguration _jwtConfig;

        public SqlServerAccountRepository(ContactsMgmtContext db, IMapper mapper, IOptions<JwtConfiguration> options)
        {
            Db = db; // TODO: put this in RepositoryBase
            Mapper = mapper; // TODO: put this in RepositoryBase
            _jwtConfig = options.Value;
        }

        public Task<string> LoginAsync(AccountLoginRequest rq)
        {
            return Task.Run(() =>
            {
                if (rq.UserName == "aaa" && rq.Password == "bbb") // mockup login
                {
                    return GenerateJwt(rq);
                }
                else
                {
                    throw new InvalidOperationException("Login failed");
                }
            });
        }

        private string GenerateJwt(AccountLoginRequest rq)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(_jwtConfig.Key,
              _jwtConfig.Issuer,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            string tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenResult;
        }
    }
}
