using AspnetCoreSPATemplate.Common;
using AspnetCoreSPATemplate.Util;
using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Repositories
{
    public class CsvContactRepository : IContactRepository
    {
        /// <summary>
        /// CSV file path (full path)
        /// </summary>
        public string FilePath { get; set; }

        public IFileLoader FileLoader { get; set; }

        public CsvContactRepository()
        {
            // No need to expose FilePath and FileLoader in constructor's parameters
            // as we don't want to make user concerned about where to get them.
            // Since they're properties so user can still change them later on if
            // they don't like the implementation.
            FilePath = AppDomain.CurrentDomain.BaseDirectory + "SampleData.csv";
            FileLoader = new CSVFileLoader(FilePath);
        }

        public async Task<IList<Contact>> GetAllContacts()
        {
            // Load data from csv file
            string fileData = await FileLoader.LoadFile();

            return await Task.Run(() => 
            {
                return ParseDataString(fileData);
            });
        }

        public async Task<IList<Contact>> GetContacts(string filter)
        {
            // Load data from csv file
            string fileData = await FileLoader.LoadFile(); 

            return await Task.Run(() =>
            {
                return ParseDataString(fileData)
                        .Where(c => c.First.Contains(filter)
                                 || c.Last.Contains(filter)
                                 || c.Email.Contains(filter)
                                 || c.Phone1.Contains(filter))
                        .ToList(); ;
            });
        }

        private List<Contact> ParseDataString(string csvData)
        {
            List<Contact> contacts = new List<Contact>();
            string[] lines = csvData.Split(
                new[] { Environment.NewLine }, // split into lines based on NewLine character
                StringSplitOptions.None        // possible to contain empty string
            );

            // skip the first row (header)
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    contacts.Add(ParseContactString(lines[i], i));
                }
                catch (Exception)
                {
                    // Skip the bad record, log it, and move to the next record
                    // Console.WriteLine($"Unable to parse record: {line}")
                }
            }

            return contacts;
        }

        private Contact ParseContactString(string contactData, int id)
        {
            string[] elements = contactData.Split(',');

            // Only extract first, last, email and phone1 as per exercise's  requirement
            Contact contact = new Contact()
            {
                Id = id,
                First = elements[0],
                Last = elements[1],
                Email = elements[9],
                Phone1 = elements[7]
            };

            return contact;
        }
    }
}
