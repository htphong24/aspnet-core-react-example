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
    public class AuthenticationLoginRequest : RequestBase
    {
        public AuthenticationLoginRequest()
            : base()
        {


        }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
