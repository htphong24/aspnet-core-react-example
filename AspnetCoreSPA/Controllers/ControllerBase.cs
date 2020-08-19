using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AspnetCoreSPATemplate.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ServiceContext Context => new ServiceContext(HttpContext);
        protected readonly IMapper Mapper;
    }
}