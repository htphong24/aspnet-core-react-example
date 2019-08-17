using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Common
{
    public class JwtConfiguration
    {
        public string Key { get; set; }

        public double ExpireDays { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

    }
}
