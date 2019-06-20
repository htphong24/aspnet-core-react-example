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
        public IFileLoader FileLoader { get; set; }
        public string FilePath { get; set; }

        public CsvContactRepository()
        {
            FilePath = AppDomain.CurrentDomain.BaseDirectory + "SampleData.csv";
            FileLoader = new CSVFileLoader(FilePath);
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            string fileData = FileLoader.LoadFile();
            return await Task.Run(() => 
            {
                return ParseDataString(fileData); ;
            });
        }

        public async Task<List<Contact>> GetContacts(string filter)
        {
            string fileData = FileLoader.LoadFile();
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
            var contacts = new List<Contact>();
            var lines = csvData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            int id = 0;

            foreach (string line in lines)
            {
                try
                {
                    id++;
                    contacts.Add(ParseContactString(line, id));
                }
                catch (Exception)
                {
                    // Skip the bad record, log it, and move to the next record
                    // log.Write($"Unable to parse record: {line}")
                }
            }
            return contacts;
        }

        private Contact ParseContactString(string contactData, int id)
        {
            var elements = contactData.Split(',');
            var contact = new Contact()
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
