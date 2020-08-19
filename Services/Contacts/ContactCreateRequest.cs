// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactCreateRequest : RequestBase
    {
        public ContactModel Contact { get; set; }
    }
}