using AspnetCoreSPATemplate.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Utils
{
    public class CsvFileHandler : IFileHandler
    {
        private string _filePath;

        public CsvFileHandler(string filePath)
        {
            _filePath = filePath;
        }

        public Task<string> LoadFile()
        {
            using (StreamReader reader = new StreamReader(path: _filePath))
            {
                return reader.ReadToEndAsync();
            }
        }

        public Task AddLine(string value)
        {
            using (StreamWriter writer = new StreamWriter(path: _filePath, append: true))
            {
                return writer.WriteLineAsync(value: value);
            }
        }
    }
}
