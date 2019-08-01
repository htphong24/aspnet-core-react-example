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

        private List<ContactModel> _contacts;

        public CsvContactRepository()
        {
            // No need to expose FilePath and FileLoader in constructor's parameters
            // as we don't want to make user concerned about where to get them.
            // Since they're properties so user can still change them later on if
            // they don't like the implementation.
            FilePath = AppDomain.CurrentDomain.BaseDirectory + "SampleData.csv";
            FileHandler = new CsvFileHandler(filePath: FilePath);
        }

        public Task<List<ContactModel>> ListAsync(ContactListRequest request)
        {
            // Load data from csv file
            string fileData = FileHandler.LoadFile().Result;
            _contacts = ParseDataString(fileData);

            List<ContactModel> result = _contacts
                                          .Skip(count: request.SkipCount)
                                          .Take(count: request.TakeCount)
                                          .ToList();

            return Task.FromResult(result: result);
        }

        public Task<int> ListPageCountAsync(ContactListRequest request)
        {
            if (_contacts == null)
            {
                // Load data from csv file
                string fileData = FileHandler.LoadFile().Result;
                _contacts = ParseDataString(fileData);
            }

            int recordCount = _contacts.Count();

            return Task.FromResult(result: (recordCount + request.RowsPerPage - 1) / request.RowsPerPage);
        }

        public Task<int> ListRecordCountAsync()
        {
            if (_contacts == null)
            {
                // Load data from csv file
                string fileData = FileHandler.LoadFile().Result;
                _contacts = ParseDataString(fileData);
            }

            return Task.FromResult(_contacts.Count());
        }

        public Task<List<ContactModel>> SearchAsync(ContactSearchRequest request)
        {
            if (_contacts == null)
            {
                // Load data from csv file
                string fileData = FileHandler.LoadFile().Result;
                _contacts = ParseDataString(fileData);
            }

            List<ContactModel> result = _contacts
                                          .Where(predicate: c => c.First.Contains(request.Query)
                                                              || c.Last.Contains(request.Query)
                                                              || c.Email.Contains(request.Query)
                                                              || c.Phone1.Contains(request.Query))
                                          .Skip(count: request.SkipCount)
                                          .Take(count: request.TakeCount)
                                          .ToList();
            return Task.FromResult(result: result);
        }

        public Task<int> SearchRecordCountAsync(ContactSearchRequest request)
        {
            if (_contacts == null)
            {
                // Load data from csv file
                string fileData = FileHandler.LoadFile().Result;
                _contacts = ParseDataString(fileData);
            }

            int recordCount = _contacts
                                .Where(predicate: c => c.First.Contains(request.Query)
                                                    || c.Last.Contains(request.Query)
                                                    || c.Email.Contains(request.Query)
                                                    || c.Phone1.Contains(request.Query))
                                .Count();
            return Task.FromResult(result: recordCount);
        }

        public Task CreateAsync(ContactCreateRequest request)
        {
            ContactModel contact = request.Contact;
            List<string> propList = new List<string>();
            return Task.Run(() =>
            {
                // read through each properties of the contact
                foreach (PropertyInfo prop in contact.GetType().GetProperties())
                {
                    object propValue = prop.GetValue(obj: contact);
                    // then add each properties to the list
                    propList.Add(item: propValue.ToString());
                }
                // then join them to a string with "," as the delimiter
                FileHandler.AddLine(value: string.Join(separator: ",", propList.ToArray()));
            });
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
                First = elements[header["first_name"]],
                Last = elements[header["last_name"]],
                Email = elements[header["email"]],
                Phone1 = elements[header["phone1"]]
            };

            return contact;
        }
    }
}
