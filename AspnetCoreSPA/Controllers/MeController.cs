using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Models;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Services;
using System.Web.Http;
using AspnetCoreSPATemplate.Services.Me;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using SqlServerDataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace AspnetCoreSPATemplate.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireStandard")]
    public class MeController : ControllerBase
    {
        private readonly IMeRepository _meRepo;

        public MeController(
            IMeRepository userRepo
        )
        {
            _meRepo = userRepo;
        }

        // http://localhost:5000/api/v1/me/
        [HttpGet]
        public async Task<ActionResult> Get([FromRoute]MeGetRequest rq)
        {
            try
            {
                rq.Id = MiscHelper.GetUserId(this.Context.User);
                MeGetResponse rs = await (new MeGetService(this.Context, _meRepo)).RunAsync(rq);
                return new ApiActionResult(this.Context.Request, rs);
            }
            catch (Exception ex)
            {
                return new ApiActionResult(this.Context.Request, ex);
            }
        }

        // http://localhost:5000/api/v1/me/
        //[HttpPatch]
        //public async Task<ActionResult> Update([FromBody]MeUpdateRequest rq)
        //{
        //    try
        //    {
        //        MeUpdateResponse rs = await (new MeUpdateService(this.Context, _meRepo)).RunAsync(rq);
        //        return new ApiActionResult(this.Context.Request, rs);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiActionResult(this.Context.Request, ex);
        //    }
        //}

    }
}
