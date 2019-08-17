using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace AspnetCoreSPATemplate.Services
{
    public class CsvContactRepository : IContactRepository
    {
        /// <summary>
        /// CSV file path (full path)
        /// </summary>
        public string FilePath { get; set; }

        public IFileHandler FileHandler { get; set; }

        public CsvContactRepository()
        {
            // No need to expose FilePath and FileLoader in constructor's parameters
            // as we don't want to make user concerned about where to get them.
            // Since they're properties so user can still change them later on if
            // they don't like the implementation.
            FilePath = AppDomain.CurrentDomain.BaseDirectory + "SampleData.csv";
            FileHandler = new CsvFileHandler(FilePath);
        }

        public async Task<List<ContactModel>> ListAsync(ContactListRequest rq)
        {
            // Load data from csv file
            string fileData = await FileHandler.LoadFileAsync();
            List<ContactModel> allContacts = ParseDataString(fileData);

            List<ContactModel> result = allContacts
                                          .Skip(rq.SkipCount)
                                          .Take(rq.TakeCount)
                                          .ToList();

            return result;
        }

        public async Task<int> ListRecordCountAsync()
        {
            // Load data from csv file
            string fileData = await FileHandler.LoadFileAsync();
            List<ContactModel> allContacts = ParseDataString(fileData);

            return allContacts.Count();
        }

        public async Task<List<ContactModel>> SearchAsync(ContactSearchRequest rq)
        {
            // Load data from csv file
            string fileData = await FileHandler.LoadFileAsync();
            List<ContactModel> allContacts = ParseDataString(fileData);

            List<ContactModel> result = allContacts
                                          .Where(predicate: c => c.FirstName.Contains(rq.Query)
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
            // Load data from csv file
            string fileData = await FileHandler.LoadFileAsync();
            List<ContactModel> allContacts = ParseDataString(fileData);

            int recordCount = allContacts
                                .Where(predicate: c => c.FirstName.Contains(rq.Query)
                                                    || c.LastName.Contains(rq.Query)
                                                    || c.Email.Contains(rq.Query)
                                                    || c.Phone1.Contains(rq.Query))
                                .Count();
            return recordCount;
        }

        public async Task CreateAsync(ContactCreateRequest rq)
        {
            ContactModel contact = rq.Contact;
            List<string> propList = new List<string>();
            // read through each properties of the contact
            foreach (PropertyInfo prop in contact.GetType().GetProperties())
            {
                object propValue = prop.GetValue(obj: contact);
                // then add each properties to the list
                propList.Add(item: propValue.ToString());
            }
            // then join them to a string with "," as the delimiter
            await FileHandler.AddLineAsync(value: string.Join(separator: ",", propList.ToArray()));
        }

        private List<ContactModel> ParseDataString(string csvData)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            string[] lines = csvData.Split(
                separator: new[] { Environment.NewLine }, // split into lines based on NewLine character
                options: StringSplitOptions.None        // possible to contain empty string
            );

            string[] columnNames = lines[0].Split(separator: ',');
            Dictionary<string, int> header = columnNames
                                                .Select(selector: (column, index) => new { column, index })
                                                .ToDictionary(keySelector: a => a.column, elementSelector: a => a.index);

            // skip the first row (header)
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    contacts.Add(item: ParseContactString(contactData: lines[i], id: i, header: header));
                }
                catch (Exception)
                {
                    // Skip the bad record, log it, and move to the next record
                    // Console.WriteLine($"Unable to parse record: {line}")
                }
            }

            return contacts;
        }

        private ContactModel ParseContactString(string contactData, int id, Dictionary<string, int> header)
        {
            string[] elements = contactData.Split(separator: ',');

            // Only extract first, last, email and phone1 as per exercise's  requirement
            ContactModel contact = new ContactModel()
            {
                Id = int.Parse(elements[header["id"]]),
                FirstName = elements[header["first_name"]],
                LastName = elements[header["last_name"]],
                Email = elements[header["email"]],
                Phone1 = elements[header["phone1"]]
            };

            return contact;
        }
    }
}
