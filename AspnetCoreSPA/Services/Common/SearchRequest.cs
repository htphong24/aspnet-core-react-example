using Microsoft.AspNetCore.Mvc;
using AspnetCoreSPATemplate.Utils;

namespace AspnetCoreSPATemplate.Services.Common
{
    [BindProperties(SupportsGet = true)]
    public class SearchRequest : ListRequest
    {
        public SearchRequest()
        {
        }

        private string _query;

        [BindProperty(Name = "q")]
        public string Query
        {
            get => _query;
            set => _query = value.Trim(100);
        }
    }
}
