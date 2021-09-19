using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Models.Ownership;

namespace Roblox.Services.Database
{
    public class OwnershipDatabase : IOwnershipDatabase
    {
        private IDatabaseConnectionProvider db { get; set; }
        
        public OwnershipDatabase(DatabaseConfiguration<dynamic> config)
        {
            db = config.dbConnection;
        }

        private const string _selectColumns = "id as userAssetId, user_id as userId, asset_id as assetId, price, serial_number as serialNumber, expires_at as expires, created_at as created, updated_at as updated";
        
        public async Task<OwnershipEntry> InsertEntry(CreateRequest request)
        {
            return await db.connection.QuerySingleOrDefaultAsync<OwnershipEntry>(
                "INSERT INTO user_asset (asset_id, user_id, price, serial_number, expires_at) VALUES (@asset_id, @user_id, null, @serial_number, @expires_at) RETURNING " +
                _selectColumns, new
                {
                    asset_id = request.assetId,
                    user_id = request.userId,
                    serial_number = request.serialNumber,
                    expires_at = request.expires,
                });
        }

        public async Task<IEnumerable<Models.Ownership.OwnershipEntry>> GetEntriesByUser(long userId, long assetId)
        {
            return await db.connection.QueryAsync<OwnershipEntry>(
                "SELECT " + _selectColumns +
                " FROM user_asset WHERE user_id = @user_id AND asset_id = @asset_id LIMIT 1", new
                {
                    user_id = userId,
                    asset_id = assetId,
                });
        }
    }
}