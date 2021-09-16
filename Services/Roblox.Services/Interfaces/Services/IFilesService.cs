using System.IO;
using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IFilesService
    {
        Task<string> CreateFileHash(Stream fileStream);
        Task UploadFile(Stream fileStream, string fileHash, string mimeOverride);
        Task<Stream> GetFile(string fileId);
    }
}