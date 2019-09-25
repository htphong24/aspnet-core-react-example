using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class ServiceContext
    {
        public HttpRequest Request { get; }

        public HttpResponse Response { get; }

        public ClaimsPrincipal User { get; }

        public ServiceContext(HttpContext httpContext)
        {
            Request = httpContext.Request;
            Response = httpContext.Response;
            User = httpContext.User;
        }
    }
}
