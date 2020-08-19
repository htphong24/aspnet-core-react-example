using System.IO;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class CsvFileHandler : IFileHandler
    {
        private readonly string _filePath;

        public CsvFileHandler(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<string> LoadFileAsync()
        {
            using StreamReader reader = new StreamReader(path: _filePath);

            return await reader.ReadToEndAsync();
        }

        public async Task AddLineAsync(string value)
        {
            using StreamWriter writer = new StreamWriter(path: _filePath, append: true);

            await writer.WriteLineAsync(value: value);
        }
    }
}