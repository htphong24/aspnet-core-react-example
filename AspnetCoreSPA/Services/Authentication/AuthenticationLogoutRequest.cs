using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services
{
    [BindProperties(SupportsGet = true)]
    public class AuthenticationLogoutRequest : RequestBase
    {
        public AuthenticationLogoutRequest()
            : base()
        {


        }

        public string Email { get; set; }

    }
}
