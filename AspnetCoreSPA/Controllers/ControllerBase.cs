using AspnetCoreSPATemplate.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ServiceContext Context => new ServiceContext(HttpContext);
    }
}