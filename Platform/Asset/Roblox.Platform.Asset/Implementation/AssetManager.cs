using System;
using System.Threading.Tasks;
using Roblox.Assets.Client;
using Roblox.AssetVersions.Client;
using Roblox.Files.Client;
using Roblox.Marketplace.Client;

namespace Roblox.Platform.Asset
{
    public class AssetManager : IAssetManager
    {
        private IAssetsV1Client assetsClient { get; set; }
        private IMarketplaceV1Client marketplaceClient { get; set; }
        private IFilesV1Client filesClient { get; set; }
        private IAssetVersionsV1Client assetVersionsClient { get; set; }

        public AssetManager(IAssetsV1Client assetsV1Client, IMarketplaceV1Client marketplaceV1Client, IFilesV1Client filesV1Client, IAssetVersionsV1Client assetVersionsV1Client)
        {
            assetsClient = assetsV1Client;
            marketplaceClient = marketplaceV1Client;
            filesClient = filesV1Client;
            assetVersionsClient = assetVersionsV1Client;
        }
        
        public async Task<Models.CreateAssetResponse> CreateAsset(Models.CreateAssetRequest request)
        {
            // todo: add rollbacks if something fails...
            // insert asset
            var asset = await assetsClient.CreateAsset(new()
            {
                name = request.name,
                description = request.description,
                assetType = request.assetType,
                creatorId = request.creatorId,
                creatorType = request.creatorType,
            });
            // add genres
            await assetsClient.SetAssetGenres(asset.assetId, request.genres);
            // upload the asset
            var file = await filesClient.UploadFile("application/binary", request.file);
            // create a new asset version
            var version = await assetVersionsClient.CreateAssetVersion(new()
            {
                assetId = asset.assetId,
                fileId = file,
                userId = request.userId,
            });
            if (request.economyInfo != null)
            {
                // add economy data (todo)
                throw new NotImplementedException();
            }
            // todo: add to the userId's inventory (or should we expect the caller to do that?...)
            // todo: request a thumbnail if the assetType needs a thumbnail (or should the caller do that?...)

            throw new NotImplementedException();
        }
    }
}