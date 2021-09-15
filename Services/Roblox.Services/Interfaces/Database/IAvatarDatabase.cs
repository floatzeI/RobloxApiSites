using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Roblox.Services.DatabaseCache;

namespace Roblox.Services.Database
{
    public interface IAvatarDatabase
    {
        Task<Models.Avatar.DbAvatarEntry> GetUserAvatar(long userId);
        Task<IEnumerable<long>> GetAvatarAssets(long userId);
    }
}