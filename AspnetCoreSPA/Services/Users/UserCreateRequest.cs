using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserCreateRequest : RequestBase
    {
        public UserCreateModel User { get; set; }
    }
}