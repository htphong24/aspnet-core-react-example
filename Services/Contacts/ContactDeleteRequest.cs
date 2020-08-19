// ReSharper disable CheckNamespace

namespace Services
{
    public class ContactDeleteRequest : RequestBase
    {
        public ContactModel Contact { get; set; }
    }
}