using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services.Common
{
    [BindProperties(SupportsGet = true)]
    public abstract class GetReferenceDataRequest : RequestBase
    {
        protected GetReferenceDataRequest()
            : base()
        {
        }

        public int? Id { get; set; }
    }
}