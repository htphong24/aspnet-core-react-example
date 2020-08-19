using Microsoft.AspNetCore.Mvc;
// ReSharper disable CheckNamespace

namespace Services
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