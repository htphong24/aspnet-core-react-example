using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Utils;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class SearchRequest : ListRequest
    {
        public SearchRequest()
        {
        }

        private string _searchCriteria;

        public string SearchCriteria
        {
            get { return _searchCriteria; }
            set { _searchCriteria = value.Trim(100); }
        }
    }
}
