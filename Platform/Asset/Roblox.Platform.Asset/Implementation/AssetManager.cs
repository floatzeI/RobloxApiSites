using System;
using System.Threading.Tasks;
using Roblox.Assets.Client;
using Roblox.AssetVersions.Client;
using Roblox.Files.Client;
using Roblox.Marketplace.Client;
using Roblox.Ownership.Client;

namespace Roblox.Platform.Asset
{
    public class AssetManager : IAssetManager
    {
        private IAssetsV1Client assetsClient { get; set; }
        private IMarketplaceV1Client marketplaceClient { get; set; }
        private IFilesV1Client filesClient { get; set; }
        private IAssetVersionsV1Client assetVersionsClient { get; set; }
        private IOwnershipClient ownershipClient { get; set; }

        public AssetManager(IAssetsV1Client assetsV1Client, IMarketplaceV1Client marketplaceV1Client, IFilesV1Client filesV1Client, IAssetVersionsV1Client assetVersionsV1Client, IOwnershipClient ownershipClientV1)
        {
            assetsClient = assetsV1Client;
            marketplaceClient = marketplaceV1Client;
            filesClient = filesV1Client;
            assetVersionsClient = assetVersionsV1Client;
            ownershipClient = ownershipClientV1;
        }
        
        public async Task<Models.CreateAssetResponse> CreateAsset(Models.CreateAssetRequest request)
        {
            // todo: add rollbacks if something fails...
            // insert asset
            var response = new Models.CreateAssetResponse();
            var asset = await assetsClient.CreateAsset(new()
            {
                name = request.name,
                description = request.description,
                assetType = request.assetType,
                creatorId = request.creatorId,
                creatorType = request.creatorType,
            });
            response.assetId = asset.assetId;
            // add genres
            await assetsClient.SetAssetGenres(asset.assetId, request.genres);
            // upload the asset
            var file = await filesClient.UploadFile("application/octet-stream", request.file);
            // create a new asset version
            var version = await assetVersionsClient.CreateAssetVersion(new()
            {
                assetId = asset.assetId,
                fileId = file,
                userId = request.userId,
            });
            response.assetVersionId = version.assetVersionId;
            
            if (request.economyInfo != null)
            {
                // add economy data
                var product = await marketplaceClient.SetProduct(new()
                {
                    assetId = asset.assetId,
                    priceInRobux = request.economyInfo.priceInRobux,
                    priceInTickets = request.economyInfo.priceInTickets,
                    isForSale = request.economyInfo.isForSale,
                    isLimited = request.economyInfo.isLimited,
                    isLimitedUnique = request.economyInfo.isLimitedUnique,
                    offSaleDeadline = request.economyInfo.offSaleDeadline,
                    stockCount = request.economyInfo.stockCount,
                });
                response.productId = product.productId;
            }

            var userAssetId = await ownershipClient.CreateEntry(new()
            {
                userId = request.userId,
                assetId = asset.assetId,
                serialNumber = null,
                expires = null
            });
            response.userAssetId = userAssetId.userAssetId;
            // todo: request a thumbnail if the assetType needs a thumbnail (or should the caller do that?...)

            return response;
        }
    }
}