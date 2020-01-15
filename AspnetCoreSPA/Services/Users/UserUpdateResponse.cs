using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserUpdateResponse : ResponseBase
    {
        public UserUpdateModel User { get; set; }
    }
}