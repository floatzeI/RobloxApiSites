using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.DatabaseCache;

namespace Roblox.Services.Database
{
    public class AvatarDatabase : IAvatarDatabase
    {
        private IAvatarDatabaseCache cache { get; set; }
        private IDatabaseConnectionProvider db { get; set; }
        
        public AvatarDatabase(DatabaseConfiguration<IAvatarDatabaseCache> config)
        {
            db = config.dbConnection;
            cache = config.dbCache;
        }

        public async Task<Models.Avatar.DbAvatarEntry> GetUserAvatar(long userId)
        {
            return await db.connection.QuerySingleOrDefaultAsync<Models.Avatar.DbAvatarEntry>(
                "SELECT avatar_type as type, head_color_id as \"headColorId\", torso_color_id as \"torsoColorId\", left_arm_color_id as \"leftArmColorId\", right_arm_color_id as \"rightArmColorId\", left_leg_color_id as \"leftLegColorId\", right_leg_color_id as \"rightLegColorId\", height_scale as height, width_scale as width, head_scale as head, depth_scale as depth, proportion_scale as proportion, body_type_scale as \"bodyType\" FROM avatar WHERE user_id = @user_id LIMIT 1", new
                {
                    user_id = userId,
                });
        }

        public async Task<IEnumerable<long>> GetAvatarAssets(long userId)
        {
            var assets = await db.connection.QueryAsync<Models.Avatar.DbAssetEntry>(
                "SELECT asset_id as assetId FROM avatar_asset WHERE user_id = @user_id", new
                {
                    user_id = userId,
                });
            return assets.Select(c => c.assetId);
        }
    }
}