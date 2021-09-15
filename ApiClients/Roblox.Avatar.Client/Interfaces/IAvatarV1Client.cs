using System.Threading.Tasks;
using Roblox.Avatar.Client.Models;

namespace Roblox.Avatar.Client
{
    public interface IAvatarV1Client
    {
        Task<AvatarEntry> GetUserAvatar(long userId);
        Task SetUserAvatar(SetAvatarRequest request);
    }
}