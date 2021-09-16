using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Models.Assets;
using Roblox.Services.Models.AssetVersions;

namespace Roblox.Services.Database
{
    public class AssetVersionsDatabase : IAssetVersionsDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        public AssetVersionsDatabase(DatabaseConfiguration<dynamic> connectionProvider)
        {
            db = connectionProvider.dbConnection;
        }
        
        public async Task<AssetVersionEntry> InsertAssetVersion(InsertAssetVersionRequest request)
        {
            return await db.connection.QuerySingleOrDefaultAsync<AssetVersionEntry>(@"INSERT INTO asset_version 
            (file_id, asset_id, user_id, version_number) 
            VALUES 
            (@file_id, @asset_id, @user_id, (SELECT COALESCE(MAX(version_number), 0) FROM asset_version WHERE asset_id = @asset_id) + 1) 
            RETURNING id as assetVersionId, asset_id as assetId, file_id as fileId, user_id as userId, version_number as versionNumber, created_at as created", new
            {
                file_id = request.fileId,
                asset_id = request.assetId,
                user_id = request.userId,
            });
        }

        public async Task DeleteAssetVersion(long assetVersionId)
        {
            await db.connection.ExecuteAsync("DELETE FROM asset_version WHERE id = @id", new
            {
                id = assetVersionId,
            });
        }

        public async Task<AssetVersionEntry> GetLatestAssetVersion(long assetId)
        {
            return await db.connection.QuerySingleOrDefaultAsync<AssetVersionEntry>(
                "SELECT id as assetVersionId, asset_id as assetId, file_id as fileId, user_id as userId, version_number as versionNumber, created_at as created FROM asset_version WHERE asset_id = @asset_id ORDER BY id DESC LIMIT 1",
                new
                {
                    asset_id = assetId,
                });
        }
    }
}