using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    [BindProperties(SupportsGet = true)]
    public abstract class GetReferenceDataRequest : RequestBase
    {
        public GetReferenceDataRequest() 
            : base()
        {
        }

        public int? Id { get; set; }
    }
}
