using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserGetResponse : ResponseBase
    {
        public UserModel User { get; set; }
    }
}