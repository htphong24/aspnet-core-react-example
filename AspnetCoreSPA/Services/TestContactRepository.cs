using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services
{
    public class TestContactRepository : IContactRepository
    {
        private List<ContactModel> _contacts;

        public TestContactRepository()
        {
            _contacts = new List<ContactModel>();
        }

        public Task<List<ContactModel>> ListAsync(ContactListRequest request)
        {
            return Task.Run(() =>
            {
                if (_contacts.Count == 0)
                {
                    LoadContacts();
                }

                List<ContactModel> result = _contacts
                                              .Skip(request.SkipCount)
                                              .Take(request.TakeCount)
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

        public Task<List<ContactModel>> SearchAsync(ContactSearchRequest request)
        {
            return Task.Run(() =>
            {
                if (_contacts.Count == 0)
                {
                    LoadContacts();
                }

                List<ContactModel> result = _contacts
                                              .Where(c => c.FirstName.Contains(request.Query)
                                                       || c.LastName.Contains(request.Query)
                                                       || c.Email.Contains(request.Query)
                                                       || c.Phone1.Contains(request.Query))
                                              .Skip(request.SkipCount)
                                              .Take(request.TakeCount)
                                              .ToList();

                return result;
            });
        }

        public Task<int> SearchRecordCountAsync(ContactSearchRequest request)
        {
            return Task.Run(() =>
            {
                if (_contacts.Count == 0)
                {
                    LoadContacts();
                }
                
                int recordCount = _contacts
                                    .Where(c => c.FirstName.Contains(request.Query)
                                             || c.LastName.Contains(request.Query)
                                             || c.Email.Contains(request.Query)
                                             || c.Phone1.Contains(request.Query))
                                    .Count();

                return recordCount;
            });
        }

        public Task CreateAsync(ContactCreateRequest request)
        {
            return Task.Run(() => _contacts.Add(request.Contact));
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
