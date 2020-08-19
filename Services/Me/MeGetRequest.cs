// ReSharper disable CheckNamespace

namespace Services
{
    public class MeGetRequest : RequestBase
    {
        public MeGetRequest()
            : base()
        {
        }

        public string Id { get; set; }
    }
}