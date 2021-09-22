using System.Threading.Tasks;
using Roblox.Avatar.Client.Models;

namespace Roblox.Platform.Rendering
{
    public interface IRenderingManager
    {
        /// <summary>
        /// Delete all thumbnails and icons for the userId
        /// </summary>
        /// <param name="userId">The userId</param>
        Task DeleteAvatarThumbnails(long userId);
        /// <summary>
        /// Render an avatar thumbnail and save it to the database.
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="resolution">The resolution</param>
        /// <param name="avatar">The avatar of the user</param>
        /// <param name="force">If true, a new render will be forced</param>
        Task RenderAvatarThumbnail(long userId, int resolution, AvatarEntry avatar, bool force = false);

        /// <summary>
        /// Render an avatar headshot and save it to the database/
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="resolution">The resolution</param>
        /// <param name="avatar">The avatar of the user</param>
        /// <param name="force">If true, a new render will be forced</param>
        Task RenderAvatarHeadshot(long userId, int resolution, AvatarEntry avatar, bool force = false);
        
        /// <summary>
        /// Render an asset thumbnail
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="resolution"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        Task RenderAssetThumbnail(long assetId, int resolution, bool force = false);
    }
}