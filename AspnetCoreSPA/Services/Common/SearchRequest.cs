using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Mvc;

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
            get { return _query; }
            set { _query = value.Trim(100); }
        }
    }
}
