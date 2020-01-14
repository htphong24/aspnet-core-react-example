using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Me
{
    public class MeGetResponse : ResponseBase
    {
        public UserModel User { get; set; }
    }
}
