using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserGetRequest : RequestBase
    {
        public UserGetRequest()
            : base()
        {

        }

        public string Id { get; set; }
    }
}
