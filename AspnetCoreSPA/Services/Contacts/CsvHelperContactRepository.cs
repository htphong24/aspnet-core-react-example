using AspnetCoreSPATemplate.Models;
using AspnetCoreSPATemplate.Services.Common;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Services.Contacts
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

        public async Task<List<ContactModel>> ListAsync(ContactListRequest rq)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            List<ContactModel> result = allContacts
                .Skip(rq.SkipCount)
                .Take(rq.TakeCount)
                .ToList();

            return result;
        }

        public async Task<int> ListPageCountAsync(ContactListRequest rq)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            int recordCount = allContacts.Count();

            return (recordCount + rq.RowsPerPage - 1) / rq.RowsPerPage;
        }

        public async Task<int> ListRecordCountAsync()
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            int recordCount = allContacts.Count();

            return recordCount;
        }

        public async Task<List<ContactModel>> SearchAsync(ContactSearchRequest rq)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            List<ContactModel> result = allContacts
                .Where(c => c.FirstName.Contains(rq.Query)
                            || c.LastName.Contains(rq.Query)
                            || c.Email.Contains(rq.Query)
                            || c.Phone1.Contains(rq.Query))
                .Skip(rq.SkipCount)
                .Take(rq.TakeCount)
                .ToList();

            return result;
        }

        public async Task<int> SearchRecordCountAsync(ContactSearchRequest rq)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);
            int recordCount = allContacts.Count(c => c.FirstName.Contains(rq.Query)
                                                     || c.LastName.Contains(rq.Query)
                                                     || c.Email.Contains(rq.Query)
                                                     || c.Phone1.Contains(rq.Query));

            return recordCount;
        }

        public async Task CreateAsync(ContactCreateRequest rq)
        {
            List<ContactModel> allContacts = await ParseContactDataAsync(FilePath);

            if (IsEmailInUse(allContacts, rq.Contact.Email))
                throw new Exception("Email is in use.");

            rq.Contact.Id = MaxId(allContacts) + 1;
            using StreamWriter writer = new StreamWriter(path: FilePath, append: true);

            using CsvWriter csv = new CsvWriter(writer);

            csv.WriteRecord(rq.Contact);
            await csv.NextRecordAsync();
        }

        public bool IsEmailInUse(List<ContactModel> contacts, string email)
        {
            bool emailFound = contacts.Any(c => c.Email == email);

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
            using StreamReader reader = new StreamReader(FilePath);

            using CsvReader csv = new CsvReader(reader);

            csv.Configuration.Delimiter = ",";
            //csv.Configuration.MissingFieldFound = null;

            // Read the first row (i.e. header)
            await csv.ReadAsync();
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

            return contacts;
        }
    }
}