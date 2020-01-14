using CsvHelper.Configuration;

namespace AspnetCoreSPATemplate.Models
{
    public sealed class ContactMap : ClassMap<ContactModel>
    {
        public ContactMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.FirstName).Index(1).Name("first_name");
            Map(m => m.LastName).Index(2).Name("last_name");
            Map(m => m.Company).Index(3).Name("company_name");
            Map(m => m.Address).Index(4).Name("address");
            Map(m => m.City).Index(5).Name("city");
            Map(m => m.State).Index(6).Name("state");
            Map(m => m.Post).Index(7).Name("post");
            Map(m => m.Phone1).Index(8).Name("phone1");
            Map(m => m.Phone2).Index(9).Name("phone2");
            Map(m => m.Email).Index(10).Name("email");
            Map(m => m.Web).Index(11).Name("web");
        }
    }
}
