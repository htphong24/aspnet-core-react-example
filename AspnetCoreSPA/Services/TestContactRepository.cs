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
            LoadContacts();
            List<ContactModel> result = _contacts
                                          .Skip(request.SkipCount)
                                          .Take(request.TakeCount)
                                          .ToList();

            return Task.FromResult(result);
        }

        public Task<int> ListPageCountAsync(ContactListRequest request)
        {
            if (_contacts == null)
            {
                LoadContacts();
            }
            int recordCount = _contacts.Count();
            return Task.FromResult((recordCount + request.RowsPerPage - 1) / request.RowsPerPage);
        }

        public Task<int> ListRecordCountAsync()
        {
            if (_contacts == null)
            {
                LoadContacts();
            }
            return Task.FromResult(_contacts.Count());
        }

        public Task<List<ContactModel>> SearchAsync(ContactSearchRequest request)
        {
            if (_contacts == null)
            {
                LoadContacts();
            }
            List<ContactModel> result = _contacts
                                          .Where(c => c.First.Contains(request.Query)
                                                   || c.Last.Contains(request.Query)
                                                   || c.Email.Contains(request.Query)
                                                   || c.Phone1.Contains(request.Query))
                                          .Skip(request.SkipCount)
                                          .Take(request.TakeCount)
                                          .ToList();

            return Task.FromResult(result);
        }

        public Task<int> SearchRecordCountAsync(ContactSearchRequest request)
        {
            if (_contacts == null)
            {
                LoadContacts();
            }
            int recordCount = _contacts
                                .Where(c => c.First.Contains(request.Query)
                                         || c.Last.Contains(request.Query)
                                         || c.Email.Contains(request.Query)
                                         || c.Phone1.Contains(request.Query))
                                .Count();

            return Task.FromResult(recordCount);
        }

        public Task CreateAsync(ContactCreateRequest request)
        {
            return Task.Run(() => _contacts.Add(request.Contact));
        }

        private void LoadContacts()
        {
            _contacts.Add(new ContactModel
            {
                First = "MF1",
                Last = "ML1",
                Email = "ME1@abc.com",
                Phone1 = "900000001"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF2",
                Last = "ML2",
                Email = "ME2@abc.com",
                Phone1 = "900000002"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF3",
                Last = "ML3",
                Email = "ME3@abc.com",
                Phone1 = "900000003"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF4",
                Last = "ML4",
                Email = "ME4@abc.com",
                Phone1 = "900000004"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF5",
                Last = "ML5",
                Email = "ME5@abc.com",
                Phone1 = "900000005"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF6",
                Last = "ML6",
                Email = "ME6@abc.com",
                Phone1 = "900000006"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF7",
                Last = "ML7",
                Email = "ME7@abc.com",
                Phone1 = "900000007"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF8",
                Last = "ML8",
                Email = "ME8@abc.com",
                Phone1 = "900000008"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF9",
                Last = "ML9",
                Email = "ME9@abc.com",
                Phone1 = "900000009"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF10",
                Last = "ML10",
                Email = "ME10@abc.com",
                Phone1 = "9000000010"
            });
            _contacts.Add(new ContactModel
            {
                First = "MF11",
                Last = "ML11",
                Email = "ME3@abc.com",
                Phone1 = "9000000011"
            });
        }
    }
}
