using Newtonsoft.Json.Converters;

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