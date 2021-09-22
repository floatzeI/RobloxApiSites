using System.Threading.Tasks;
using Roblox.Rendering.Client.Models;

namespace Roblox.Rendering.Client
{
    public interface IRenderingClient
    {
        /// <summary>
        /// Render an avatar thumbnail
        /// </summary>
        /// <param name="request">The <see cref="Models.RenderAvatarRequest"/></param>
        /// <returns>The <see cref="Models.RenderAvatarResponse"/></returns>
        Task<RenderAvatarResponse> RenderAvatarThumbnail(RenderAvatarRequest request);

        /// <summary>
        /// Render an avatar headshot
        /// </summary>
        /// <param name="request">The <see cref="Models.RenderAvatarRequest"/></param>
        /// <returns>The <see cref="Models.RenderAvatarResponse"/></returns>
        Task<RenderAvatarResponse> RenderAvatarHeadshot(RenderAvatarRequest request);

        /// <summary>
        /// Render a square asset thumbnail
        /// </summary>
        /// <param name="assetId">ID of the asset to render</param>
        /// <param name="resolution">Resolution</param>
        /// <returns></returns>
        Task<RenderAvatarResponse> RenderAsset(long assetId, int resolution);
    }
}