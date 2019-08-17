using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services.Common
{
    [BindProperties(SupportsGet = true)]
    public class AccountLoginRequest : RequestBase
    {
        public AccountLoginRequest()
            : base()
        {


        }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
