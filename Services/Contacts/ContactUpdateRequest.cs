// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactUpdateRequest : RequestBase
    {
        public ContactModel Contact { get; set; }
    }
}