using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace AspnetCoreSPATemplate.Services
{
    public class CsvHelperContactRepository : IContactRepository
    {
        /// <summary>
        /// CSV file path (full path)
        /// </summary>
        public string FilePath { get; set; }

        public CsvHelperContactRepository()
        {
            // No need to expose FilePath and FileLoader in constructor's parameters
            // as we don't want to make user concerned about where to get them.
            // Since they're properties so user can still change them later on if
            // they don't like the implementation.
            FilePath = AppDomain.CurrentDomain.BaseDirectory + "SampleData.csv";
        }

        public async Task<List<ContactModel>> ListAsync(ContactListRequest request)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            List<ContactModel> result = allContacts
                                          .Skip(request.SkipCount)
                                          .Take(request.TakeCount)
                                          .ToList();
            return result;
        }

        public async Task<int> ListPageCountAsync(ContactListRequest request)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            int recordCount = allContacts.Count();
            return (recordCount + request.RowsPerPage - 1) / request.RowsPerPage;
        }

        public async Task<int> ListRecordCountAsync()
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            int recordCount = allContacts.Count();
            return recordCount;
        }

        public async Task<List<ContactModel>> SearchAsync(ContactSearchRequest request)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            List<ContactModel> result = allContacts
                                          .Where(c => c.FirstName.Contains(request.Query)
                                                   || c.LastName.Contains(request.Query)
                                                   || c.Email.Contains(request.Query)
                                                   || c.Phone1.Contains(request.Query))
                                          .Skip(request.SkipCount)
                                          .Take(request.TakeCount)
                                          .ToList();

            return result;
        }

        public async Task<int> SearchRecordCountAsync(ContactSearchRequest request)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            int recordCount = allContacts
                                .Where(c => c.FirstName.Contains(request.Query)
                                         || c.LastName.Contains(request.Query)
                                         || c.Email.Contains(request.Query)
                                         || c.Phone1.Contains(request.Query))
                                .Count();

            return recordCount;
        }

        public async Task CreateAsync(ContactCreateRequest request)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            if (IsEmailInUse(allContacts, request.Contact.Email))
            {
                throw new Exception("Email is in use.");
            }
            else
            {
                request.Contact.Id = MaxId(allContacts) + 1;
                using (StreamWriter writer = new StreamWriter(path: FilePath, append: true))
                using (CsvWriter csv = new CsvWriter(writer))
                {
                    csv.WriteRecord(request.Contact);
                    csv.NextRecord();
                }
            }
        }

        public bool IsEmailInUse(List<ContactModel> contacts, string email)
        {
            bool emailFound = contacts
                                .Where(c => c.Email == email)
                                .Any();

            return emailFound;
        }

        public int MaxId(List<ContactModel> contacts)
        {
            return contacts.Max(c => c.Id);
        }

        private async Task<List<ContactModel>> ParseContactDataAsync(string filePath)
        {
            List<ContactModel> contacts = new List<ContactModel>();

            // Load data from csv file
            using (StreamReader reader = new StreamReader(FilePath))
            {
                using (CsvReader csv = new CsvReader(reader))
                {
                    csv.Configuration.Delimiter = ",";
                    //csv.Configuration.MissingFieldFound = null;

                    // Read the first row (i.e. header)
                    csv.Read();
                    // Read the header record without reading the first row
                    csv.ReadHeader();
                    while (await csv.ReadAsync())
                    {
                        ContactModel contact = new ContactModel
                        {
                            Id = int.Parse(csv.GetField("id")),
                            FirstName = csv.GetField("first_name"),
                            LastName = csv.GetField("last_name"),
                            Email = csv.GetField("email"),
                            Phone1 = csv.GetField("phone1")
                        };
                        contacts.Add(contact);
                    }
                }
            }

            return contacts;
        }

    }
}
