using System.Threading.Tasks;

namespace Roblox.Services.Services
{
    public interface IAvatarService
    {
        Task<Models.Avatar.AvatarEntry> GetUserAvatar(long userId);
        Task SetUserAvatar(Models.Avatar.SetAvatarRequest request);
    }
}