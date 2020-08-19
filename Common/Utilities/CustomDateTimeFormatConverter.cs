using Newtonsoft.Json.Converters;

namespace Common.Utilities
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