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

        public async Task InsertUserAvatar(Models.Avatar.SetAvatarRequest request)
        {
            await db.connection.ExecuteAsync(
                "INSERT INTO avatar (user_id, head_color_id, torso_color_id, left_arm_color_id, right_arm_color_id, left_leg_color_id, right_leg_color_id, height_scale, width_scale, head_scale, depth_scale, proportion_scale, body_type_scale, avatar_type) VALUES (@user_id, @head_color_id, @torso_color_id, @left_arm_color_id, @right_arm_color_id, @left_leg_color_id, @right_leg_color_id, @height_scale, @width_scale, @head_scale, @depth_scale, @proportion_scale, @body_type_scale, @avatar_type)",
                new
                {
                    user_id = request.userId,
                    head_color_id = request.colors.headColorId,
                    torso_color_id = request.colors.torsoColorId,
                    left_arm_color_id = request.colors.leftArmColorId,
                    right_arm_color_id = request.colors.rightArmColorId,
                    left_leg_color_id = request.colors.leftLegColorId,
                    right_leg_color_id = request.colors.rightLegColorId,
                    height_scale = request.scales.height,
                    width_scale = request.scales.width,
                    head_scale = request.scales.head,
                    depth_scale = request.scales.depth,
                    proportion_scale = request.scales.proportion,
                    body_type_scale = request.scales.bodyType,
                    avatar_type = request.type,
                });
        }
        
        public async Task UpdateUserAvatar(Models.Avatar.SetAvatarRequest request)
        {
            await db.connection.ExecuteAsync(
                "UPDATE avatar SET head_color_id = @head_color_id, torso_color_id = @torso_color_id, left_arm_color_id = @left_arm_color_id, right_arm_color_id = @right_arm_color_id, left_leg_color_id = @left_leg_color_id, right_leg_color_id = @right_leg_color_id, head_scale = @head_scale, proportion_scale = @proportion_scale, width_scale = @width_scale, height_scale = @height_scale, body_type_scale = @body_type_scale, avatar_type = @avatar_type, depth_scale = @depth_scale WHERE user_id = @user_id",
                new
                {
                    user_id = request.userId,
                    head_color_id = request.colors.headColorId,
                    torso_color_id = request.colors.torsoColorId,
                    left_arm_color_id = request.colors.leftArmColorId,
                    right_arm_color_id = request.colors.rightArmColorId,
                    left_leg_color_id = request.colors.leftLegColorId,
                    right_leg_color_id = request.colors.rightLegColorId,
                    height_scale = request.scales.height,
                    width_scale = request.scales.width,
                    head_scale = request.scales.head,
                    depth_scale = request.scales.depth,
                    proportion_scale = request.scales.proportion,
                    body_type_scale = request.scales.bodyType,
                    avatar_type = request.type,
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

        public async Task InsertAvatarAsset(long userId, long assetId)
        {
            await db.connection.ExecuteAsync(
                "INSERT INTO avatar_asset (user_id, asset_id) VALUES (@user_id, @asset_id)", new
                {
                    user_id = userId,
                    asset_id = assetId,
                });
        }
        
        public async Task DeleteAvatarAsset(long userId, long assetId)
        {
            await db.connection.ExecuteAsync(
                "DELETE FROM avatar_asset WHERE user_id = @user_id AND asset_id = @asset_id", new
                {
                    user_id = userId,
                    asset_id = assetId,
                });
        }
    }
}