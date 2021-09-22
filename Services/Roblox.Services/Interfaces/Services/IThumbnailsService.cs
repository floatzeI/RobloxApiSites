using System.Threading.Tasks;
using Roblox.Services.Exceptions.Services;

namespace Roblox.Services.Services
{
    public interface IThumbnailsService
    {
        /// <summary>
        /// Insert a thumbnail
        /// </summary>
        /// <param name="request">Your insertion request</param>
        Task InsertThumbnail(Models.Thumbnails.ThumbnailEntry request);

        /// <summary>
        /// Delete thumbnails matching the referenceId
        /// </summary>
        /// <param name="referenceId">The ID (e.g. "UserId" or "assetId")</param>
        /// <param name="thumbnailType">The thumbnailId</param>
        Task DeleteThumbnailsForReference(long referenceId, int thumbnailType);
        /// <summary>
        /// (Debug) Get a thumbnail by it's referenceId and type, with any resolution
        /// </summary>
        /// <param name="referenceId">The referenceId</param>
        /// <param name="thumbnailType">The type</param>
        /// <returns>The <see cref="Models.Thumbnails.ThumbnailEntry"/>, or throws <see cref="RecordNotFoundException"/></returns>
        Task<Models.Thumbnails.ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType);
        /// <summary>
        /// Get thumbnail with specific X/Y resolution
        /// </summary>
        /// <param name="referenceId">The reference id</param>
        /// <param name="thumbnailType">The type</param>
        /// <param name="resolutionX">The X resolution</param>
        /// <param name="resolutionY">The Y resolution</param>
        /// <returns>The <see cref="Models.Thumbnails.ThumbnailEntry"/>, or throws <see cref="RecordNotFoundException"/></returns>
        Task<Models.Thumbnails.ThumbnailEntry> GetThumbnail(long referenceId, int thumbnailType, int resolutionX,
            int resolutionY);
        
        /// <summary>
        /// Get a thumbnail from the MD5 hash.
        /// </summary>
        /// <param name="thumbnailHash">The thumbnailHash</param>
        /// <param name="resolutionX">The X resolution</param>
        /// <param name="resolutionY">The Y resolution</param>
        /// <returns></returns>
        Task<Models.Thumbnails.ThumbnailEntry> GetThumbnailByHash(string thumbnailHash, int resolutionX,
            int resolutionY);
    }
}