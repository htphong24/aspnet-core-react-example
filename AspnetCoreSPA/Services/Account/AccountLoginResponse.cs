using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class AccountLoginResponse : RequestBase
    {
        public AccountLoginResponse()
            : base()
        {

        }

        public string Token { get; set; }

        //public User User { get; set; }

    }
}
