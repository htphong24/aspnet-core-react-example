using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;

namespace AspnetCoreSPATemplate.Services.Contacts
{
    public class TestContactRepository : IContactRepository
    {
        private List<ContactModel> _contacts;

        public TestContactRepository()
        {
            _contacts = new List<ContactModel>();
        }

        public Task<List<ContactModel>> ListAsync(ContactListRequest rq)
        {
            return Task.Run(() =>
            {
                if (_contacts.Count == 0)
                {
                    LoadContacts();
                }

                List<ContactModel> result = _contacts
                                              .Skip(rq.SkipCount)
                                              .Take(rq.TakeCount)
                                              .ToList();

                return result;
            });
        }

        public Task<int> ListRecordCountAsync()
        {
            return Task.Run(() =>
            {
                if (_contacts.Count == 0)
                {
                    LoadContacts();
                }

                return _contacts.Count();
            });
        }

        public Task<List<ContactModel>> SearchAsync(ContactSearchRequest rq)
        {
            return Task.Run(() =>
            {
                if (_contacts.Count == 0)
                {
                    LoadContacts();
                }

                List<ContactModel> result = _contacts
                                              .Where(c => c.FirstName.Contains(rq.Query)
                                                       || c.LastName.Contains(rq.Query)
                                                       || c.Email.Contains(rq.Query)
                                                       || c.Phone1.Contains(rq.Query))
                                              .Skip(rq.SkipCount)
                                              .Take(rq.TakeCount)
                                              .ToList();

                return result;
            });
        }

        public Task<int> SearchRecordCountAsync(ContactSearchRequest rq)
        {
            return Task.Run(() =>
            {
                if (_contacts.Count == 0)
                {
                    LoadContacts();
                }

                int recordCount = _contacts
                                    .Where(c => c.FirstName.Contains(rq.Query)
                                             || c.LastName.Contains(rq.Query)
                                             || c.Email.Contains(rq.Query)
                                             || c.Phone1.Contains(rq.Query))
                                    .Count();

                return recordCount;
            });
        }

        public Task CreateAsync(ContactCreateRequest rq)
        {
            return Task.Run(() => _contacts.Add(rq.Contact));
        }

        private void LoadContacts()
        {
            _contacts.Add(new ContactModel
            {
                Id = 101,
                FirstName = "MF1",
                LastName = "ML1",
                Email = "ME1@abc.com",
                Phone1 = "900000001"
            });
            _contacts.Add(new ContactModel
            {
                Id = 102,
                FirstName = "MF2",
                LastName = "ML2",
                Email = "ME2@abc.com",
                Phone1 = "900000002"
            });
            _contacts.Add(new ContactModel
            {
                Id = 103,
                FirstName = "MF3",
                LastName = "ML3",
                Email = "ME3@abc.com",
                Phone1 = "900000003"
            });
            _contacts.Add(new ContactModel
            {
                Id = 104,
                FirstName = "MF4",
                LastName = "ML4",
                Email = "ME4@abc.com",
                Phone1 = "900000004"
            });
            _contacts.Add(new ContactModel
            {
                Id = 105,
                FirstName = "MF5",
                LastName = "ML5",
                Email = "ME5@abc.com",
                Phone1 = "900000005"
            });
            _contacts.Add(new ContactModel
            {
                Id = 106,
                FirstName = "MF6",
                LastName = "ML6",
                Email = "ME6@abc.com",
                Phone1 = "900000006"
            });
            _contacts.Add(new ContactModel
            {
                Id = 107,
                FirstName = "MF7",
                LastName = "ML7",
                Email = "ME7@abc.com",
                Phone1 = "900000007"
            });
            _contacts.Add(new ContactModel
            {
                Id = 108,
                FirstName = "MF8",
                LastName = "ML8",
                Email = "ME8@abc.com",
                Phone1 = "900000008"
            });
            _contacts.Add(new ContactModel
            {
                Id = 109,
                FirstName = "MF9",
                LastName = "ML9",
                Email = "ME9@abc.com",
                Phone1 = "900000009"
            });
            _contacts.Add(new ContactModel
            {
                Id = 110,
                FirstName = "MF10",
                LastName = "ML10",
                Email = "ME10@abc.com",
                Phone1 = "9000000010"
            });
            _contacts.Add(new ContactModel
            {
                Id = 111,
                FirstName = "MF11",
                LastName = "ML11",
                Email = "ME3@abc.com",
                Phone1 = "9000000011"
            });
        }
    }
}
