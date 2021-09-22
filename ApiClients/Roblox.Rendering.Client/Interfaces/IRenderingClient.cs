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
    }
}