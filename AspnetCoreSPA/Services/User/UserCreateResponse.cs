using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Utils;
using Common.Identity;
using Microsoft.AspNetCore.Mvc;
using SqlServerDataAccess.EF;

namespace AspnetCoreSPATemplate.Services
{
    public class UserCreateResponse : ResponseBase
    {
        public UserCreateResponse() : base()
        {

        }

        public ApplicationUser User { get; set; }

    }
}
