using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Utilities
{
    public interface IFileHandler
    {
        Task<string> LoadFileAsync();

        Task AddLineAsync(string value);
    }
}