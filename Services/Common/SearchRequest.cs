using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable CheckNamespace

namespace Services
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