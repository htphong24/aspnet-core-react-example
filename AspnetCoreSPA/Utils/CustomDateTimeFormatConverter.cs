using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Utils
{
    public class CustomDateTimeFormatConverter : IsoDateTimeConverter
    {
        public CustomDateTimeFormatConverter(string format)
            : base()
        {
            DateTimeFormat = format;
        }
    }
}
