using AspnetCoreSPATemplate.Services;
using AspnetCoreSPATemplate.Util;
using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace AspnetCoreSPATemplate.Repositories
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

        public async Task<IList<Contact>> GetAllContactsAsync()
        {
            return await Task.Run(() => ParseContactDataAsync(FilePath));
        }

        public async Task<IList<Contact>> GetContactsAsync(string filter)
        {
            return await Task.Run(() => ParseContactDataAsync(FilePath)
                                          .Where(c => c.First.Contains(filter)
                                                   || c.Last.Contains(filter)
                                                   || c.Email.Contains(filter)
                                                   || c.Phone1.Contains(filter))
                                          .ToList()
            );
        }

        private IList<Contact> ParseContactDataAsync(string filePath)
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
