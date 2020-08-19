// ReSharper disable CheckNamespace

namespace Services
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