using System.Threading.Tasks;
using Roblox.Files.Client;
using Roblox.Platform.Thumbnail.Models;
using Roblox.Thumbnails.Client;

namespace Roblox.Platform.Thumbnail
{
    public class ThumbnailManager : IThumbnailManager
    {
        private IFilesV1Client filesClient { get; set; }
        private IThumbnailsV1Client thumbnailsClient { get; set; }

        public ThumbnailManager(IFilesV1Client filesV1Client, IThumbnailsV1Client thumbnailsV1Client)
        {
            filesClient = filesV1Client;
            thumbnailsClient = thumbnailsV1Client;
        }


        public async Task UploadPngThumbnail(ThumbnailUploadRequest request)
        {
            request.mime = "image/png";
            var fileId = await filesClient.UploadFile(request.mime, request.file);
            await thumbnailsClient.InsertThumbnail(new(request.id,request.referenceId, fileId, request.thumbnailType, request.resolutionX, request.resolutionY));
        }
    }
}