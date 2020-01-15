using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserDeleteRequest : RequestBase
    {
        public UserDeleteModel User { get; set; }
    }
}