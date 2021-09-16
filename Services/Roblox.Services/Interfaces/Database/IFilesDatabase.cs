using System.IO;
using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    public interface IFilesDatabase
    {
        Task InsertFile(string fileHash, string mimeType, long sizeInBytes);
        Task<bool> DoesFileExist(string fileHash);
    }
}