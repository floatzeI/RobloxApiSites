using System.Threading.Tasks;
using Roblox.Thumbnails.Client.Models;

namespace Roblox.Thumbnails.Client
{
    public interface IThumbnailsV1Client
    {
        Task<ThumbnailEntry> GetThumbnail(long referenceId, ThumbnailType type, int resolutionX, int resolutionY);
        Task<ThumbnailEntry> GetThumbnailByHash(string hash, int resolutionX, int resolutionY);
        Task DeleteThumbnail(long referenceId, ThumbnailType type);
        Task InsertThumbnail(ThumbnailEntry thumbnail);
    }
}