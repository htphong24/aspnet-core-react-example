using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using System.Collections.Generic;

namespace AspnetCoreSPATemplate.Services.Users
{
    public class UserListResponse : ListResponse
    {
        public UserListResponse()
            : base()
        {
        }

        /// <summary>
        /// Results of the search
        /// </summary>
        public List<UserModel> Results { get; set; }
    }
}