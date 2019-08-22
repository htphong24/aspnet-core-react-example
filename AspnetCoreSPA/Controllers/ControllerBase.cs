using AspnetCoreSPATemplate.Services;
using AspnetCoreSPATemplate.Services.Common;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlServerDataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ServiceContext Context => new ServiceContext(HttpContext);
    }
}
