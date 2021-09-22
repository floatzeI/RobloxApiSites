using System.Threading.Tasks;

namespace Roblox.Services.Database
{
    public interface IThumbnailsDatabase
    {
        Task InsertThumbnail(Models.Thumbnails.ThumbnailEntry request);
        Task DeleteThumbnailsForReference(long referenceId, int thumbnailType);
        Task<Models.Thumbnails.ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType);

        Task<Models.Thumbnails.ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType, int resolutionX,
            int resolutionY);

        Task<Models.Thumbnails.ThumbnailEntry> ResolveThumbnailHash(string thumbnailHash, int resolutionX,
            int resolutionY);
    }
}