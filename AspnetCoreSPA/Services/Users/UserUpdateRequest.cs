using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserUpdateRequest : RequestBase
    {
        public UserUpdateModel User { get; set; }
    }
}