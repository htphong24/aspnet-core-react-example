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

        public string First { get; set; }

        public string Last { get; set; }

        public string Email { get; set; }

        public string Phone1 { get; set; }
    }
}
