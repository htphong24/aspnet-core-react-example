using AspnetCoreSPATemplate.Services;
using AspnetCoreSPATemplate.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ServiceContext Context => new ServiceContext(HttpContext);
    }
}
