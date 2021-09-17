using System.Threading.Tasks;
using Roblox.Files.Client;
using Roblox.Platform.Thumbnail.Models;

namespace Roblox.Platform.Thumbnail
{
    public class ThumbnailManager : IThumbnailManager
    {
        private IFilesV1Client filesClient { get; set; }

        public ThumbnailManager(IFilesV1Client filesV1Client)
        {
            filesClient = filesV1Client;
        }


        public async Task UploadPngThumbnail(ThumbnailUploadRequest request)
        {
            request.mime = "image/png";
            // todo: first, upload thumbnail to files service
            // then send the data over to thumbnail service
            throw new System.NotImplementedException();
        }
    }
}