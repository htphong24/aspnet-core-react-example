using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Models
{
    public class ContactMap : ClassMap<Contact>
    {
        public ContactMap()
        {
            Map(m => m.First).Index(0).Name("first_name");
            Map(m => m.Last).Index(1).Name("last_name");
            Map(m => m.Company).Index(2).Name("company_name");
            Map(m => m.Address).Index(3).Name("address");
            Map(m => m.City).Index(4).Name("city");
            Map(m => m.State).Index(5).Name("state");
            Map(m => m.Post).Index(6).Name("post");
            Map(m => m.Phone1).Index(7).Name("phone1");
            Map(m => m.Phone2).Index(8).Name("phone2");
            Map(m => m.Email).Index(9).Name("email");
            Map(m => m.Web).Index(10).Name("web");
        }
    }
}
