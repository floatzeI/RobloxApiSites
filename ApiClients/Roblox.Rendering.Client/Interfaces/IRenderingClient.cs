using System.Threading.Tasks;
using Roblox.Rendering.Client.Models;

namespace Roblox.Rendering.Client
{
    public interface IRenderingClient
    {
        /// <summary>
        /// Render an avatar thumbnail
        /// </summary>
        /// <remarks>
        /// This operation is slow. It may take anywhere from 5 seconds to 60 seconds, or more, to return a response.
        /// </remarks>
        /// <param name="request">The <see cref="Models.RenderAvatarRequest"/></param>
        /// <returns>The <see cref="Models.RenderAvatarResponse"/></returns>
        Task<RenderAvatarResponse> RenderAvatarThumbnail(RenderAvatarRequest request);
    }
}