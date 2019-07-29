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

        public Task<List<ContactModel>> ListAsync(ContactListRequest request)
        {
            List<ContactModel> result = ParseContactData(FilePath)
                                      .Skip(request.SkipCount)
                                      .Take(request.TakeCount)
                                      .ToList();
            return Task.FromResult(result);
        }

        public Task<int> ListPageCountAsync(ContactListRequest request)
        {
            int recordCount = ParseContactData(FilePath).Count();
            return Task.FromResult((recordCount + request.RowsPerPage - 1) / request.RowsPerPage);
        }

        public Task<List<ContactModel>> SearchAsync(ContactSearchRequest request)
        {
            List<ContactModel> result = ParseContactData(FilePath)
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
            int recordCount = ParseContactData(FilePath)
                                .Where(c => c.First.Contains(request.Query)
                                         || c.Last.Contains(request.Query)
                                         || c.Email.Contains(request.Query)
                                         || c.Phone1.Contains(request.Query))
                                .Count();
            return Task.FromResult(recordCount);
        }

        public Task CreateAsync(ContactCreateRequest request)
        {
            if (IsEmailInUse(request.Contact.Email))
            {
                throw new Exception("Email is in use.");
            }
            else
            {
                return Task.Run(() =>
                {
                    using (StreamWriter writer = new StreamWriter(path: FilePath, append: true))
                    using (CsvWriter csv = new CsvWriter(writer))
                    {
                        csv.WriteRecord(request.Contact);
                    }
                });
            }
        }

        public bool IsEmailInUse(string email)
        {
            int foundContacts = ParseContactData(FilePath)
                                  .Where(c => c.Email == email)
                                  .Count();
            return foundContacts > 0;
        }

        private List<ContactModel> ParseContactData(string filePath)
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
                    while (csv.Read())
                    {
                        ContactModel contact = new ContactModel
                        {
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
