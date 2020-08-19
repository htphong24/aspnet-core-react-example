using Newtonsoft.Json.Converters;

namespace AspnetCoreSPATemplate.Utilities
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