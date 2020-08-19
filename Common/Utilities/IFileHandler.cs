using System.Threading.Tasks;

namespace Common.Utilities
{
    public interface IFileHandler
    {
        Task<string> LoadFileAsync();

        Task AddLineAsync(string value);
    }
}