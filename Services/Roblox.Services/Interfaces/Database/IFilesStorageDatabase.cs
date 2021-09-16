using System.IO;
using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    /// <summary>
    /// A service that you can fetch, upload, delete, and overwrite files.
    /// </summary>
    public interface IFilesStorageDatabase
    {
        /// <summary>
        /// Get a file by its ID
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<Stream> GetFileById(string fileId);

        /// <summary>
        /// Upload a file using the specified mime type.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileStream">The file to upload</param>
        /// <param name="mimeType">The mime type of the file (e.g. "Image/PNG")</param>
        Task UploadFile(string id, Stream fileStream, string mimeType);

        /// <summary>
        /// Delete a file by its ID
        /// </summary>
        /// <param name="id">The ID to delete</param>
        Task DeleteFile(string id);
    }
}