using System.Threading.Tasks;
using Roblox.Services.Database;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models.AssetVersions;

namespace Roblox.Services.Services
{
    public class AssetVersionsService : IAssetVersionsService
    {
        private IAssetVersionsDatabase db { get; set; }

        public AssetVersionsService(IAssetVersionsDatabase assetVersionsDatabase)
        {
            db = assetVersionsDatabase;
        }


        public async Task<AssetVersionEntry> InsertAssetVersion(InsertAssetVersionRequest request)
        {
            return await db.InsertAssetVersion(request);
        }

        public async Task DeleteAssetVersion(long assetVersionId)
        {
            await db.DeleteAssetVersion(assetVersionId);
        }

        public async Task<AssetVersionEntry> GetLatestAssetVersion(long assetId)
        {
            var result = await db.GetLatestAssetVersion(assetId);
            if (result == null) throw new RecordNotFoundException(assetId);
            return result;
        }
    }
}