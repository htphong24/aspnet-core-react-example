using AspnetCoreSPATemplate.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Utils
{
    public class CsvFileLoader : IFileLoader
    {
        private string _filePath;

        public CsvFileLoader(string filePath)
        {
            _filePath = filePath;
        }

        public Task<string> LoadFile()
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                return reader.ReadToEndAsync();
            }
        }
    }
}
