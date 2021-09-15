using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.DatabaseCache;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Lib.Extensions;

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

        public async Task SetUserAvatar(Models.Avatar.SetAvatarRequest request)
        {
            var userId = request.userId;
            var assets = request.assetIds.ToList().Distinct().ToList();
            // first, get current avatar, figure out the assetIds dif, then insert/remove the assets
            var current = (await db.GetAvatarAssets(userId)).ToList();
            var toRemove = ListExtensions.GetItemsNotInSecondList(
                current, 
                assets, 
                (idOne, idTwo) => idOne == idTwo
            );
            var toAdd = ListExtensions.GetItemsNotInSecondList(
                assets, 
                current, 
                (idOne, idTwo) => idOne == idTwo
            );

            // Submit changes
            foreach (var item in toRemove)
            {
                await db.DeleteAvatarAsset(userId, item);
            }

            foreach (var item in toAdd)
            {
                await db.InsertAvatarAsset(userId, item);
            }

            var hasPreviousAvatar = current.Count > 0 || await db.GetUserAvatar(userId) != null;
            if (hasPreviousAvatar)
            {
                await db.UpdateUserAvatar(request);
            }
            else
            {
                await db.InsertUserAvatar(request);
            }
        }
    }
}