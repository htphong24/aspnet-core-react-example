using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Models
{
    public class Contact
    {
        /// <summary>
        /// For list in react app as it usually requires a unique key
        /// </summary>
        public int Id { get; set; }

        [Name("first_name")]
        public string First { get; set; }

        [Name("last_name")]
        public string Last { get; set; }

        [Name("email")]
        public string Email { get; set; }

        [Name("phone1")]
        public string Phone1 { get; set; }
    }
}
