using System;
using System.Threading.Tasks;
using Roblox.Assets.Client;
using Roblox.Marketplace.Client;

namespace Roblox.Platform.Asset
{
    public class AssetManager : IAssetManager
    {
        private IAssetsV1Client assetsClient { get; set; }
        private IMarketplaceV1Client marketplaceClient { get; set; }

        public AssetManager(IAssetsV1Client assetsV1Client, IMarketplaceV1Client marketplaceV1Client)
        {
            assetsClient = assetsV1Client;
            marketplaceClient = marketplaceV1Client;
        }
        
        public async Task<Models.CreateAssetResponse> CreateAsset(Models.CreateAssetRequest request)
        {
            throw new NotImplementedException();
        }
    }
}