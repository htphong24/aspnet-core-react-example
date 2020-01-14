using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Me
{
    public class MeGetRequest : RequestBase
    {
        public MeGetRequest()
            : base()
        {

        }

        public string Id { get; set; }
    }
}
