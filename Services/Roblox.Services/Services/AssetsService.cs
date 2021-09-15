using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.Assets;

namespace Roblox.Services.Services
{
    public class AssetsService : IAssetsService
    {
        private IAssetsDatabase db { get; set; }

        public AssetsService(IAssetsDatabase db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AssetEntry>> MultiGetAssetsById(IEnumerable<long> assetIds)
        {
            var ids = assetIds.AsList().Distinct().ToArray();
            // sanity checks
            if (ids.Length == 0) throw new ArgumentException("Must specify at least 1 ID", nameof(assetIds));
            if (ids.Length > 500)
                throw new ArgumentException("This operation is not safe. ID Count = " + ids.Length, nameof(assetIds));
            return await db.MultiGetAssetsById(ids);
        }

        public async Task<AssetEntry> GetAssetById(long assetId)
        {
            var dbResult = (await db.MultiGetAssetsById(new long[] { assetId })).ToArray();
            if (dbResult.Length == 0) throw new RecordNotFoundException(assetId);
            return dbResult[0];
        }

        public async Task<AssetEntry> InsertAsset(InsertAssetRequest request)
        {
            var result = await db.InsertAsset(request);
            return new()
            {
                assetId = result.assetId,
                name = request.name,
                description = request.description,
                creatorId = request.creatorId,
                creatorType = request.creatorType,
                created = result.created,
                updated = result.created,
            };
        }

        public async Task UpdateAsset(UpdateAssetRequest request)
        {
            await db.UpdateAsset(request);
        }
    }
}