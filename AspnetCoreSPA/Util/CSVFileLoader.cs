using AspnetCoreSPATemplate.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Util
{
    public class CsvFileLoader : IFileLoader
    {
        private string _filePath;

        public CsvFileLoader(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<string> LoadFile()
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
