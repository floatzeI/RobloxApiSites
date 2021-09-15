using System;
using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.DatabaseCache;
using Roblox.Services.Exceptions.Services;

namespace Roblox.Services.Services
{
    public class AvatarService : IAvatarService
    {
        private IAvatarDatabase db { get; set; }

        public AvatarService(IAvatarDatabase db)
        {
            this.db = db;
        }

        public async Task<Models.Avatar.AvatarEntry> GetUserAvatar(long userId)
        {
            var result = await db.GetUserAvatar(userId);
            if (result == null) throw new RecordNotFoundException();
            var assets = await db.GetAvatarAssets(userId);
            return new()
            {
                colors = new()
                {
                    headColorId = result.headColorId,
                    torsoColorId = result.torsoColorId,
                    leftArmColorId = result.leftArmColorId,
                    rightArmColorId = result.rightArmColorId,
                    leftLegColorId = result.leftLegColorId,
                    rightLegColorId = result.rightLegColorId,
                },
                scales = new()
                {
                    bodyType = result.bodyType,
                    depth = result.depth,
                    head = result.head,
                    height = result.height,
                    proportion = result.proportion,
                    width = result.width,
                },
                type = result.type,
                assetIds = assets,
            };
        }
    }
}