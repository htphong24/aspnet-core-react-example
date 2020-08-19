using System.Collections.Generic;
// ReSharper disable CheckNamespace

namespace Services
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