﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class ContactSearchResponse: SearchResponse
    {
        public ContactSearchResponse()
            : base()
        {
        }

        /// <summary>
        /// Results of the search
        /// </summary>
        public IList<Contact> Results { get; set; }

    }
}
