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

        public Task<List<Contact>> ListAsync(ContactListRequest request)
        {
            List<Contact> result = ParseContactDataAsync(FilePath)
                                      .Skip(request.SkipCount)
                                      .Take(request.TakeCount)
                                      .ToList();
            return Task.FromResult(result);
        }

        public Task<int> ListPageCountAsync(ContactListRequest request)
        {
            int recordCount = ParseContactDataAsync(FilePath).Count();
            return Task.FromResult((recordCount + request.RowsPerPage - 1) / request.RowsPerPage);
        }

        public Task<List<Contact>> SearchAsync(ContactSearchRequest request)
        {
            List<Contact> result = ParseContactDataAsync(FilePath)
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
            int recordCount = ParseContactDataAsync(FilePath)
                                .Where(c => c.First.Contains(request.Query)
                                         || c.Last.Contains(request.Query)
                                         || c.Email.Contains(request.Query)
                                         || c.Phone1.Contains(request.Query))
                                .Count();
            return Task.FromResult(recordCount);
        }

        private List<Contact> ParseContactDataAsync(string filePath)
        {
            List<Contact> contacts = new List<Contact>();

            // Load data from csv file
            using (StreamReader reader = new StreamReader(FilePath))
            {
                using (CsvReader csv = new CsvReader(reader))
                {
                    csv.Configuration.Delimiter = ",";
                    //csv.Configuration.MissingFieldFound = null;
                    int i = 0;

                    // Read the first row (i.e. header)
                    csv.Read();
                    // Read the header record without reading the first row
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        Contact contact = new Contact
                        {
                            Id = i++,
                            First = csv.GetField("first_name"),
                            Last = csv.GetField("last_name"),
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
