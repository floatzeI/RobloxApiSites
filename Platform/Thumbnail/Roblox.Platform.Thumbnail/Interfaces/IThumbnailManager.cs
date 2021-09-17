using System.IO;
using System.Threading.Tasks;
using Roblox.Platform.Thumbnail.Models;

namespace Roblox.Platform.Thumbnail
{
    public interface IThumbnailManager
    {
        /// <summary>
        /// Upload a PNG thumbnail
        /// </summary>
        /// <param name="request">The upload request</param>
        Task UploadPngThumbnail(ThumbnailUploadRequest request);
    }
}