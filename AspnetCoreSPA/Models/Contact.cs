using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Models
{
    public class Contact
    {
        public string First { get; set; }

        public string Last { get; set; }

        public string Company { get; }

        public string Address { get; }

        public string City { get; }

        public string State { get; }

        public string Post { get; }
        
        public string Phone1 { get; set; }

        public string Phone2 { get; }

        public string Email { get; set; }

        public string Web { get; }
    }
}
