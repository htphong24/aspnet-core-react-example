using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class SearchResponse: ListResponse
    {
        public SearchResponse()
            : base()
        {
        }

        /// <summary>
        /// Number of matching records found
        /// </summary>
        public int RecordCount { get; set; }

    }
}
