using AspnetCoreSPATemplate.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Util
{
    public class CSVFileLoader : IFileLoader
    {
        private string _filePath;

        public CSVFileLoader(string filePath)
        {
            _filePath = filePath;
        }

        public string LoadFile()
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
