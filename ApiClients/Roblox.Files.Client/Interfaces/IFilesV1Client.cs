using System.IO;
using System.Threading.Tasks;

namespace Roblox.Files.Client
{
    public interface IFilesV1Client
    {
        /// <summary>
        /// Get a file by its fileId
        /// </summary>
        /// <param name="fileId">The 128 character fileId</param>
        /// <returns>The fileStream</returns>
        Task<Stream> GetFileById(string fileId);
        /// <summary>
        /// Upload a file. Returns the fileId
        /// </summary>
        /// <param name="mimeType">The mime type of the file (such as "Image/PNG"). This is not escaped or checked, so do not use user-provided data.</param>
        /// <param name="fileToUpload">The file stream to upload.</param>
        /// <returns>The created fileId</returns>
        Task<string> UploadFile(string mimeType, Stream fileToUpload);
    }
}