using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Thumbnails;

namespace Roblox.Services.Services
{
    public class ThumbnailsService : IThumbnailsService
    {
        private IThumbnailsDatabase thumbnailsDatabase { get; set; }

        public ThumbnailsService(IThumbnailsDatabase db)
        {
            thumbnailsDatabase = db;
        }

        public async Task InsertThumbnail(ThumbnailEntry request)
        {
            await thumbnailsDatabase.InsertThumbnail(request);
        }

        public async Task<ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType)
        {
            var result = await thumbnailsDatabase.GetThumbnail(referenceId, thumbnailType);
            if (result == null) throw new RecordNotFoundException(referenceId);
            return result;
        }

        public async Task<ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType, int resolutionX, int resolutionY)
        {
            var result = await thumbnailsDatabase.GetThumbnail(referenceId, thumbnailType, resolutionX, resolutionY);
            if (result == null) throw new RecordNotFoundException(referenceId);
            return result;
        }

        public async Task<ThumbnailEntry> GetThumbnailByHash(string thumbnailHash, int resolutionX, int resolutionY)
        {
            var result = await thumbnailsDatabase.ResolveThumbnailHash(thumbnailHash, resolutionX, resolutionY);
            if (result == null) throw new RecordNotFoundException();
            return result;
        }
    }
}